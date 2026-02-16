using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Meetups.WebApp.Shared.ViewModels
{
	public class EventViewModel
	{
		public int EventId { get; set; }

		[Required]
		[StringLength(maximumLength:100)]
		public string? Title { get; set; }

		[StringLength(maximumLength:500)]
		public string? Description { get; set; }
		
		[Required]
		public DateOnly BeginDate { get; set; }

		[Required]
		public TimeOnly BeginTime { get; set; }

		[Required]
		public DateOnly EndDate { get; set; }

		[Required]
		public TimeOnly EndTime { get; set; }

		public string? Location { get; set; }
		
		public string? MeetupLink { get; set; }

		[Required]
		public string? Category { get; set; }

		[Range(0, int.MaxValue)]
		public int Capacity { get; set; }

		[Required(ErrorMessage = "Please upload a cover image for meetup.")]
		public IBrowserFile CoverImage { get; set; }

		public string? ImageUrl { get; set; }

		public int OrganizerId { get; set; }

		public EventViewModel()
		{
			BeginDate = DateOnly.FromDateTime(DateTime.Now);
			BeginTime = TimeOnly.FromDateTime(DateTime.Now);
			EndDate = DateOnly.FromDateTime(DateTime.Now);
			EndTime = TimeOnly.FromDateTime(DateTime.Now);

			Category = MeetupCategoriesEnum.InPerson.ToString();
			ImageUrl = $"/images/NoImageFound.png";
		}

		public string? ValidateDates()
		{
			DateTime combinedBeginDateTime = BeginDate.ToDateTime(BeginTime);
			DateTime combinedEndDateTime = EndDate.ToDateTime(EndTime);

			if (combinedBeginDateTime < DateTime.Now)
			{
				return "Begin Date and Time should be in the future.";
			}

			if (BeginDate > EndDate)
			{
				return "Begin date should be earlier than End date.";
			}
			if (BeginDate == EndDate && BeginTime >= EndTime) 
			{
				return "Begin time should be earlier than End time.";
			}

			return string.Empty;
		}

		public string? ValidateLocation()
		{
			if (Category == MeetupCategoriesEnum.InPerson.ToString() && string.IsNullOrWhiteSpace(Location))
			{
				return "The Location is required for In-Person Meetups.";
			}
			return string.Empty;
		}

		public string? ValidateMeetupLink()
		{
			if (Category == MeetupCategoriesEnum.Online.ToString() && string.IsNullOrWhiteSpace(MeetupLink))
			{
				return "The Meetup link is required for Online Meetups.";
			}
			return string.Empty;
		}
	}
}
