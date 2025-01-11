namespace GYmTahaluf.Models
{
    public class MemberModel
    {
        public User User { get; set; }

        public List<User> Trainners
        {
            get; set;

        }
        public List<Exercise>Exercises { get; set; }
        public List<SubscriptionPlan> Plans;
        public Contact Contact { get; set; }
        public Testimonial Testimonial { get; set; }
        public MemberModel() { }
        public MemberModel(User user, Contact contact, List<User> trainners,List<SubscriptionPlan>plans,List<Exercise>exercises)
        {
            User = user;
            Contact = contact;
            Trainners = trainners;
            Plans = plans;
            Exercises=exercises;


        }

    }
}
