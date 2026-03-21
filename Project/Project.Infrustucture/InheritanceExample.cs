namespace Project.Infrastructure;

// Переменная хоть и имеет тип Animal, но всё ещё хранит ссылку на объект типа Cat.И так как метод переопределён через override, во время выполнения 
// рантайм идёт по этой ссылке и вызывает реализацию реального объекта, а не типа переменной — поэтому выведется "Meow". 
// Но если не указать override, то метод в Cat не считается переопределением, а просто скрывает метод базового класса, и тогда рантайм 
// смотрит на тип переменной, а не на реальный объект — поэтому будет вызван метод Animal .

public class InheritanceExample
{
    public static void Run()
    {
        Animal animal = new Cat();
        animal.makeSound();
    }
}

public abstract class Animal
{
    public virtual void makeSound()
    {
        Console.WriteLine("Some generic animal sound");
    }
}

class Cat : Animal
{
    public void makeSound()
    {
        Console.WriteLine("Meow");
    }
}   


