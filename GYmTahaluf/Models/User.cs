    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace GYmTahaluf.Models;

    public partial class User
    {
        public decimal Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; } 

        public decimal RoleId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? LastLogin { get; set; }

        public DateTime? UpdateAt { get; set; }

        public bool Isblock { get; set; }

        public string? UserImage { get; set; }
            [NotMapped]
        public virtual IFormFile imageFile { get; set; }   

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public virtual Role Role { get; set; } = null!;

        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

        public virtual ICollection<SubscriptionPlan> SubscriptionPlans { get; set; } = new List<SubscriptionPlan>();

        public virtual ICollection<Testimonial> TestimonialTrainers { get; set; } = new List<Testimonial>();

        public virtual ICollection<Testimonial> TestimonialUsers { get; set; } = new List<Testimonial>();
    }
