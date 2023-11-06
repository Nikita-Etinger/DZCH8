using System;
using System.Threading;


abstract class Car
{
    public int Distance { get; set; }
    public string Name { get; set; }
    public int Speed { get; set; }

    public Car(string name, int speed)
    {
        Name = name;
        Speed = speed;
        Distance = 0;
    }
    public abstract void Move();

    // Событие для оповещения о финише автомобиля
    public event EventHandler Finished;
    //public delegate void EventHandler(object sender, EventArgs e);
    protected virtual void OnFinished()
    {
        Finished?.Invoke(this, EventArgs.Empty);
    }
}


class SportsCar : Car
{
    public SportsCar(string name, int speed) : base(name, speed) { }

    public override void Move()
    {

        Speed = new Random().Next(1, 5);
        Distance += Speed;
        if (Distance >= 100) OnFinished(); // Оповещаем о финише
    }
}


class PassengerCar : Car
{
    public PassengerCar(string name, int speed) : base(name, speed) { }

    public override void Move()
    {

        Speed = new Random().Next(1, 4); 
        Distance += Speed;
        if (Distance >= 100) OnFinished();
    }
}


class Truck : Car
{
    public Truck(string name, int speed) : base(name, speed) { }

    public override void Move()
    {

        Speed = new Random().Next(1, 3);
        Distance += Speed;
        if (Distance >= 100) OnFinished(); 
    }
}


class Bus : Car
{
    public Bus(string name, int speed) : base(name, speed) { }

    public override void Move()
    {
       
        Speed = new Random().Next(1, 2); 
        Distance += Speed;
        if (Distance >= 100) OnFinished();
    }
}

class Program
{
    static void Main()
    {
  
        Car sportsCar = new SportsCar("Спортивный автомобиль", 0);
        Car passengerCar = new PassengerCar("Легковой автомобиль", 0);
        Car truck = new Truck("Грузовик", 0);
        Car bus = new Bus("Автобус", 0);

        //событие завершения гонки
        sportsCar.Finished += (sender, e) => Console.WriteLine($"{sportsCar.Name} пришел к финишу!");
        passengerCar.Finished += (sender, e) => Console.WriteLine($"{passengerCar.Name} пришел к финишу!");
        truck.Finished += (sender, e) => Console.WriteLine($"{truck.Name} пришел к финишу!");
        bus.Finished += (sender, e) => Console.WriteLine($"{bus.Name} пришел к финишу!");

        while (true)
        {
            
            
            sportsCar.Move();
            passengerCar.Move();
            truck.Move();
            bus.Move();

            //Console.Clear();
            DrawRace(sportsCar.Distance, passengerCar.Distance, truck.Distance, bus.Distance);

            Thread.Sleep(300); // Пауза в 0.3 с

            if (sportsCar.Distance >= 100 || passengerCar.Distance >= 100 || truck.Distance >= 100 || bus.Distance >= 100)
            {
                break;
            }
            else Console.Clear();
        }
    }

    static void DrawRace(int sportsCarPos, int passengerCarPos, int truckPos, int busPos)
    {
        Console.WriteLine($"Спортивный автомобиль:\t {new string('-', sportsCarPos)}");
        Console.WriteLine($"Легковой автомобиль:\t {new string('-', passengerCarPos)}");
        Console.WriteLine($"Грузовик:\t\t {new string('-', truckPos)}");
        Console.WriteLine($"Автобус:\t\t {new string('-', busPos)}");
    }
}