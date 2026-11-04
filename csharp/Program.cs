using System;
using System.Collections.Generic;
using System.Linq;

class FoodItem
{
    public string Name { get; }
    public int Calories { get; }

    public FoodItem(string name, int calories)
    {
        Name = name;
        Calories = calories;
    }
}

class Meal
{
    private readonly List<FoodItem> items = new List<FoodItem>();
    public string Name { get; }
    public IReadOnlyList<FoodItem> Items => items;

    public Meal(string name)
    {
        Name = name;
    }

    public void AddItem(FoodItem item)
    {
        items.Add(item);
    }

    public long TotalCalories()
    {
        return items.Sum(item => (long)item.Calories);
    }
}

class DailyIntake
{
    private readonly List<Meal> meals = new List<Meal>();

    public void AddMeal(Meal meal)
    {
        meals.Add(meal);
    }

    public long TotalCalories()
    {
        return meals.Sum(meal => meal.TotalCalories());
    }

    public void PrintSummary()
    {
        foreach (Meal meal in meals)
        {
            Console.WriteLine($"{meal.Name}: {meal.TotalCalories()} kcal");
            foreach (FoodItem item in meal.Items)
                Console.WriteLine($"  {item.Name}: {item.Calories} kcal");
        }
        Console.WriteLine($"Razem: {TotalCalories()} kcal");
    }
}

class Program
{
    static void Main()
    {
        var day = new DailyIntake();
        Console.WriteLine("Kalkulator kalorii");

        while (true)
        {
            string mealName = ReadText("Nazwa posiłku (Enter kończy dzień): ");
            if (mealName.Length == 0)
                break;
            var meal = new Meal(mealName);

            while (true)
            {
                string itemName = ReadText("Nazwa produktu (Enter kończy posiłek): ");
                if (itemName.Length == 0)
                    break;

                string value = ReadText("Kalorie w porcji: ");
                if (!int.TryParse(value, out int calories) || calories < 0)
                {
                    Console.WriteLine("Podaj nieujemną liczbę całkowitą.");
                    continue;
                }
                meal.AddItem(new FoodItem(itemName, calories));
            }

            if (meal.Items.Count > 0)
                day.AddMeal(meal);
        }
        day.PrintSummary();
    }

    static string ReadText(string prompt)
    {
        Console.Write(prompt);
        return (Console.ReadLine() ?? "").Trim();
    }
}
