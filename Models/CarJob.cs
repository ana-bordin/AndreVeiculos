﻿namespace Models
{
    public class CarJob
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public Job Job { get; set; }
        public bool Status { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Car: {Car}, Job: {Job}, Status: {Status}";
        }
    }
}
