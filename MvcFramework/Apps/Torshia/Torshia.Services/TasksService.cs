using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Torshia.Data;
using Torshia.Data.Models;
using Torshia.Data.Models.Enums;
using Torshia.Models.Tasks;

namespace Torshia.Services
{
	public class TasksService : ITasksService
	{
		private const string AffectedSectorSeperator = ", ";
		private readonly TorshiaDbContext context;

		public TasksService(TorshiaDbContext context)
		{
			this.context = context;
		}

		public void Create(TaskCreateInputModel model)
		{
			string[] participantNames = model.Participants.Split(AffectedSectorSeperator);
			HashSet<string> participantIds = new HashSet<string>();
			foreach (var participantName in participantNames)
			{
				Participant participant = context.Participants.FirstOrDefault(p => p.Name == participantName);
				if(participant == null)
				{
					participant = new Participant { Name = participantName };
					context.Participants.Add(participant);
				}

				participantIds.Add(participant.Id);
			}

			context.SaveChanges();

			HashSet<ParticipantTask> participantTasks = new HashSet<ParticipantTask>();
			foreach (var participanId in participantIds)
			{
				participantTasks.Add(new ParticipantTask { ParticipantId = participanId });
			}

			Task task = new Task
			{
				AffectedSectors = string.Join(AffectedSectorSeperator, model.AffectedSectors),
				Description = model.Description,
				Title = model.Title,
				DueDate = model.DueDate,
				Participants = participantTasks,
			};

			context.Tasks.Add(task);
			context.SaveChanges();
		}

		public TaskViewModel GetById(string id)
		{
			var task = context.Tasks
				.Where(t => t.Id == id)
				.Select(t => new TaskViewModel
				{
					AffectedSectors = t.AffectedSectors, 
					Description = t.Description,
					Title = t.Title,
					DueDate = t.DueDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
					ParticipantNames = t.Participants.Select(p => p.Participant.Name).ToList()
				})
				.FirstOrDefault();

			return task;
		}

		public IEnumerable<TaskHomeViewModel> GetAllTasks()
		{
			var tasks = context.Tasks
				.Where(t => !t.IsReported)
				.Select(t => new TaskHomeViewModel
				{
					Title = t.Title,
					AffectedSectors = t.AffectedSectors,
					Id = t.Id
				})
				.ToList();

			return tasks;
		}

		public void Report(string taskId, string userId)
		{
			Task task = context.Tasks.FirstOrDefault(t => t.Id == taskId);
			if(task == null)
			{
				return;
			}
			task.IsReported = true;
			context.Tasks.Update(task);

			Report report = new Report
			{
				ReportedOn = DateTime.UtcNow,
				ReporterId = userId,
				TaskId = taskId,
				Status = GetReportStatus(),
			};

			context.Reports.Add(report);
			context.SaveChanges();
		}

		private ReportStatus GetReportStatus()
		{
			int number = new Random().Next(0, 4);
			if(number == 0)
			{
				return ReportStatus.Archived;
			}
			return ReportStatus.Completed;
		}
	}
}
