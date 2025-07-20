using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Core.Features.Base.Create;
using MyAssistant.Core.Features.Base.Update;
using MyAssistant.Core.Features.Notifications.Create;
using MyAssistant.Domain.Lookups;
using MyAssistant.Domain.Models;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.Core.Features.Recurrences
{
    public class UpdateRecurrenceCommandHandler : IRequestHandler<UpdateRecurrenceCommand, Guid>
    {
        private const int MAX_ALLOWED_OCCURRENCES = 100;

        private readonly IMediator _mediator;
        private readonly IRecurrenceRepository _recurrenceRepo;
        private readonly IBaseAsyncRepository<TaskItem> _taskRepo;
        private readonly IMapper _mapper;

        public UpdateRecurrenceCommandHandler(
            IRecurrenceRepository recurrenceRepo, 
            IBaseAsyncRepository<TaskItem> taskRepo, 
            IMediator mediator, 
            IMapper mapper)
        {
            _recurrenceRepo = recurrenceRepo;
            _taskRepo = taskRepo;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateRecurrenceCommand request, CancellationToken cancellationToken)
        {
            // 1. Fetch existing Recurrence
            var recurrence = await _recurrenceRepo.GetByIdAsync(request.Id);
            if (recurrence == null)
                throw new KeyNotFoundException($"Recurrence with ID {request.Id} not found.");

            // 2. Update properties (using AutoMapper for simplicity)
            // Ignore Id, update all other properties from command
            _mapper.Map(request, recurrence);

            // 3. Save updated Recurrence
            await _recurrenceRepo.UpdateAsync(recurrence);

            int affectedTaskCount = 0;
            IList<TaskItem>? newTasks = null;

            // 4. Optionally update all occurrences
            if (request.UpdateAllOccurrences)
            {
                // Load all upcoming TaskItems for this recurrence into memory 
                var existingTasks = recurrence.TaskItems?.ToList().Where(x => x.DueDate.Value > DateTime.Now) ?? new List<TaskItem>();

                // Generate the (new) desired schedule
                var desiredTasks = GenerateTasksForRecurrence(recurrence).ToList();

                // Used for result reporting
                int updatedCount = 0, createdCount = 0, deletedCount = 0;

                // Build a lookup by DueDate (or your unique key for match, e.g. DueDate + Time)
                // This assumes DueDate is the unique identifier for each occurrence!
                var existingByDueDate = existingTasks.ToDictionary(t => t.DueDate.Value.Date, t => t);

                // --- 1. Update or Insert ---
                foreach (var desired in desiredTasks)
                {
                    // Try to match by DueDate
                    if (existingByDueDate.TryGetValue(desired.DueDate.Value.Date, out var existing))
                    {
                        // Update the existing TaskItem
                        existing.Title = desired.Title;
                        existing.Description = desired.Description;
                        existing.LengthInMinutes = desired.LengthInMinutes;
                        existing.Priority = desired.Priority;
                        existing.ScheduledAt = desired.ScheduledAt;
                        existing.RecurrenceId = recurrence.Id;

                        await _mediator.Send(new UpdateEntityCommand<TaskItem>(existing), cancellationToken); 
                        updatedCount++;
                    }
                    else
                    {
                        // New scheduled occurrence, create it
                        await _mediator.Send(new CreateEntityCommand<TaskItem>(desired), cancellationToken);
                        createdCount++;
                    }
                }

                // Delete TaskItems no longer in the schedule ---
                var desiredDates = desiredTasks.Select(t => t.DueDate.Value.Date).ToHashSet();
                var toDelete = existingTasks.Where(t => !desiredDates.Contains(t.DueDate.Value.Date)).ToList();
                if (toDelete.Any())
                {
                    foreach (var item in toDelete)
                        //TODO: Replace with mediator.Send(delete) and remove reference to TaskRepo
                        await _taskRepo.DeleteAsync(item);

                    deletedCount = toDelete.Count;
                }

                affectedTaskCount = updatedCount + createdCount + deletedCount;
            }
            else
            {
                // If not updating all - affected count is just for the recurrence
                affectedTaskCount = 1;
            }

            // Send a notification to the user..
            var notificationMsg = request.UpdateAllOccurrences
                ? $"Recurrence and all {affectedTaskCount} TaskItem(s) updated for {recurrence.Title}"
                : $"Recurrence updated for {recurrence.Title}";
            var createNotificationCmd = new CreateNotificationCommand(
                recurrence, 
                recurrence.UserId,
                "Recurrence updated successfully.",
                notificationMsg
            );
            await _mediator.Send(createNotificationCmd, cancellationToken);

            return recurrence.Id;
        }

        /// <summary>
        /// Generates TaskItems for the given recurrence schedule.
        /// </summary>
        private IEnumerable<TaskItem> GenerateTasksForRecurrence(Recurrence recurrence)
        {
            foreach (var date in GetRequestOccurrences(recurrence))
            {
                var item = new TaskItem
                {
                    RecurrenceId = recurrence.Id,
                    Title = recurrence.Title,
                    Description = recurrence.Description,
                    DueDate = date,
                    LengthInMinutes = recurrence.LengthInMinutes,
                    Priority = recurrence.DefaultPriority
                };

                if (recurrence.Time.HasValue)
                    item.ScheduledAt = new DateTime(date.Year, date.Month, date.Day)
                        .AddHours(recurrence.Time.Value.Hours)
                        .AddMinutes(recurrence.Time.Value.Minutes);

                yield return item;
            }
        }

        /// <summary>
        /// Generates a series of occurrence dates based on the specified recurrence pattern.
        /// </summary>
        private IEnumerable<DateTime> GetRequestOccurrences(Recurrence request)
        {
            var result = new List<DateTime>();
            var occurrence = request.StartDate;

            // Defensive: If EndDate is missing, allow up to max occurrences
            for (int count = 0; count < MAX_ALLOWED_OCCURRENCES; count++)
            {
                if (request.EndDate.HasValue && occurrence > request.EndDate.Value)
                    break;

                result.Add(occurrence);

                if(request.RecurrenceTypeCode == RecurrenceType.Daily)
                    occurrence = occurrence.AddDays(request.Interval);

                else if (request.RecurrenceTypeCode == RecurrenceType.Weekly)
                    occurrence = occurrence.AddDays(request.Interval * 7);

                else if (request.RecurrenceTypeCode == RecurrenceType.Monthly)
                    occurrence = occurrence.AddMonths(request.Interval);

                else if (request.RecurrenceTypeCode == RecurrenceType.Annually)
                    occurrence = occurrence.AddYears(request.Interval);

                else throw new ValidationException("Invalid Recurrence Type.");
            }

            return result;
        }
    }
}