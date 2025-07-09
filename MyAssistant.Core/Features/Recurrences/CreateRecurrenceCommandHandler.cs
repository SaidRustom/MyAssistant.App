using AutoMapper;
using MediatR;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Core.Features.Notifications.Create;
using MyAssistant.Domain.Lookups;
using MyAssistant.Domain.Models;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.Core.Features.Recurrences
{
    public class CreateRecurrenceCommandHandler : IRequestHandler<CreateRecurrenceCommand, Guid>
    {
        static int MAX_ALLOWED_OCCURRENCES = 100;        
        private readonly IMediator _mediator;
        private readonly IBaseAsyncRepository<Recurrence> _repo;
        private readonly IBaseAsyncRepository<TaskItem> _taskRepo;
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates a new <see cref="Recurrence"/> 
        /// and generates associated <see cref="TaskItem"/> instances for each occurrence date.
        /// Maps the command to a recurrence entity, persists it, computes each occurrence date,
        /// and adds corresponding task items to the repository.
        /// Returns the ID of the newly created recurrence.
        /// </summary>
        public CreateRecurrenceCommandHandler(IBaseAsyncRepository<Recurrence> repo, IBaseAsyncRepository<TaskItem> taskRepo ,IMediator mediator, IMapper mapper)
        {
            _repo = repo;
            _taskRepo = taskRepo;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateRecurrenceCommand request, CancellationToken cancellation)
        {

            var recurrence = _mapper.Map<Recurrence>(request);
            recurrence = await _repo.AddAsync(recurrence);

            List<TaskItem> tasks = new();
            foreach(var date in GetRequestOccurrences(recurrence))
            {
                TaskItem item = new()
                {
                    RecurrenceId = recurrence.Id,
                    Title = recurrence.Title,
                    Description = recurrence.Description,
                    DueDate = date,
                    LengthInMinutes = recurrence.LengthInMinutes,
                    Priority = recurrence.DefaultPriority
                };

                if(recurrence.Time.HasValue)
                    item.ScheduledAt = new DateTime(date.Year, date.Month, date.Day)
                        .AddHours(recurrence.Time.Value.Hours)
                        .AddMinutes(recurrence.Time.Value.Minutes);
                
                tasks.Add(item);              
            }

            var addedTaskCount = await _taskRepo.AddRangeAsync(tasks);

            CreateNotificationCommand cmd = new(recurrence, recurrence.UserId,
                "Recurrences added successfully", $"{addedTaskCount} Occurrences added for {recurrence.Title}");

            await _mediator.Send(cmd);

            return recurrence.Id;
        }

        /// <summary>
        /// Generates a series of occurrence dates based on the specified recurrence pattern.
        /// The occurrences start from the given StartDate and continue up to EndDate
        /// (if provided), or until the maximum allowed number of occurrences is reached.
        /// The recurrence type (daily, weekly, monthly, annually) and interval are defined
        /// by the Recurrence request parameter.
        /// </summary>
        public IEnumerable<DateTime> GetRequestOccurrences(Recurrence request)
        {
            List<DateTime> result = new List<DateTime>();
            DateTime occurrence = request.StartDate;

            while(occurrence <= request.EndDate && 
                (!request.EndDate.HasValue || occurrence <= request.EndDate.Value) &&
                result.Count < MAX_ALLOWED_OCCURRENCES)
            {
                //add the start date instance//
                result.Add(occurrence);

                if(request.RecurrenceTypeCode == RecurrenceType.Daily)
                    occurrence = occurrence.AddDays(request.Interval);

                if (request.RecurrenceTypeCode == RecurrenceType.Weekly)
                    occurrence = occurrence.AddDays(request.Interval * 7);

                if (request.RecurrenceTypeCode == RecurrenceType.Monthly)
                    occurrence = occurrence.AddMonths(request.Interval);

                if (request.RecurrenceTypeCode == RecurrenceType.Annually)
                    occurrence = occurrence.AddYears(request.Interval);
            }

            return result;
        }
    }
}
