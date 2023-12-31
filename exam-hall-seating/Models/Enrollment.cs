﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam_hall_seating.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        [ForeignKey("Lecture")]
        public int LectureId { get; set; }
        public Lecture? Lecture { get; set; }
        public int Year { get; set; }
        public Periods Period { get; set; }
        public Grades? Grade { get; set; }

        public enum Periods
        {
            Güz = 1,
            Bahar = 2
        }

        public enum Grades
        {
            A, B, C, D, F
        }



    }
}
