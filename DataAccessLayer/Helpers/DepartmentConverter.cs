using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Enums
{
    public static class DepartmentConverter
    {
        public static string ConvertToString(this Departments dep)
        {
            switch (dep)
            {
                case Departments.Alco:
                    return "Alcohol";
                case Departments.Baby:
                    return "Baby food";
                case Departments.Bakery:
                    return "Bakery";
                case Departments.Dairy:
                    return "Dairy";
                case Departments.Diabetic:
                    return "Diabetic food";
                case Departments.Frozen:
                    return "Frozen food";
                case Departments.FruitVeg:
                    return "Fruits and Vegatables";
                case Departments.Gastronomy:
                    return "Gastronomy";
                case Departments.Grocery:
                    return "Grocery";
                case Departments.NonAlco:
                    return "Nonalcohol drinks";
                case Departments.Pastry:
                    return "Pastry";
                case Departments.Sushi:
                    return "Sushi";
            }
            return null;
        }
    }
}
