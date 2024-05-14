namespace HDWDotNetCore.RestAPIWithNLayer.Models
{
    public class IncompatibleFoodModel
    {

        public class Incompatiblefood
        {
            public Tbl_Incompatiblefood[] Tbl_IncompatibleFood { get; set; }
        }

        public class Tbl_Incompatiblefood
        {
            public int Id { get; set; }
            public string FoodA { get; set; }
            public string FoodB { get; set; }
            public string Description { get; set; }
        }
        public class IncompatibleFoodRespondData
        {
            public int Id { get; set; }
            public string FoodA { get; set; }
            public string FoodB { get; set; }
            public string Description { get; set; }
        }
        public class IncompatibleFoodCategory
        {
            public string Description { get; set; }
        }
    }
}
