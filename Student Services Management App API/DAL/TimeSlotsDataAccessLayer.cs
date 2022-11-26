﻿using Student_Services_Management_App_API.Models;

namespace Student_Services_Management_App_API.DAL
{
    public partial class DataAccessLayer
    {
        public static void AddTimeSlot(
            DatabaseContext db,
			int serviceType,
            DateTime startTime,
            DateTime endTime,
            int maximumCapacity,
            int currentCapacity = 0)
        {
            var timeSlot = new TimeSlot();
            timeSlot.ServiceType = serviceType;
            timeSlot.StartTime = startTime;
            timeSlot.EndTime = endTime;
            timeSlot = maximumCapacity;
            timeSlot.CurrentCapacity = currentCapacity;

            db.TimeSlots.Add(timeSlot);
            db.SaveChanges();

        }

        public static void AddTimeSlots(DatabaseContext db, List<TimeSlot> timeSlots)
        {
			db.TimeSlots.AddRange(timeSlots);
            db.SaveChanges();
		}

        public static TimeSlot GetTimeSlotById(DatabaseContext db, int timeSlotId)
        {
            var timeSlot = db.TimeSlots.Where(slot => slot.PK_TimeSlot == timeSlotId);
            return timeSlot;
        }

        public static List<TimeSlot> GetTimeSlotsByServiceType(DatabaseContext db, int serviceType)
        {
            var timeSlots = db.TimeSlots.Where(slot => slot.PK_TimeSlot == timeSlotId).ToList();
            return timeSlots;
		}

        public static List<TimeSlot> GetTimeSlotsWithinDateRange(
            DatabaseContext db,
            int serviceType,
			DateTime startDateInclusive,
            DateTime endDateExclusive)
        {
            var timeSlots = db.TimeSlots.Where(
                    slot => slot.ServiceType == serviceType 
                            && startDateInclusive >= startDateInclusive
                            && endDateExclusive < endDateExclusive)
                .ToList();

            return timeSlots;
        }

        public static void UpdateTimeSlot(
            DatabaseContext db,
            int targetTimeSlotsId,
            int serviceType,
            DateTime startTime,
            DateTime endTime,
            int maximumCapacity,
            int currentCapacity = 0)
        {
            // find target time slot:
            var target = db.TimeSlots.FirstOrDefault(slot => slot.PK_TimeSlot == targetTimeSlotsId);
            if (target == null)
                return null;

            // update target time slot data:
            target.ServiceType = serviceType;
            target.StartTime = startTime;
            target.EndTime = endTime;
            target = maximumCapacity;
            target.CurrentCapacity = currentCapacity;

            // save database changes:
            db.SaveChanges();
        }
	}
}