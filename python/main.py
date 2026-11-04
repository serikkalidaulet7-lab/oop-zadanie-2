class FoodItem:
    def __init__(self, name, calories):
        self.name = name
        self.calories = calories


class Meal:
    def __init__(self, name):
        self.name = name
        self.items = []

    def add_item(self, item):
        self.items.append(item)

    def total_calories(self):
        return sum(item.calories for item in self.items)


class DailyIntake:
    def __init__(self):
        self.meals = []

    def add_meal(self, meal):
        self.meals.append(meal)

    def total_calories(self):
        return sum(meal.total_calories() for meal in self.meals)

    def print_summary(self):
        for meal in self.meals:
            print(f"{meal.name}: {meal.total_calories()} kcal")
            for item in meal.items:
                print(f"  {item.name}: {item.calories} kcal")
        print(f"Razem: {self.total_calories()} kcal")


def main():
    day = DailyIntake()
    print("Kalkulator kalorii")
    while name := input("Nazwa posiłku (Enter kończy dzień): ").strip():
        meal = Meal(name)
        while name := input("Nazwa produktu (Enter kończy posiłek): ").strip():
            value = input("Kalorie w porcji: ").strip()
            try:
                calories = int(value)
                if calories < 0:
                    raise ValueError
            except ValueError:
                print("Podaj nieujemną liczbę całkowitą.")
                continue
            meal.add_item(FoodItem(name, calories))
        if meal.items:
            day.add_meal(meal)
    day.print_summary()


if __name__ == "__main__":
    main()
