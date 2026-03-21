namespace Project.Infrastructure;

// В этом случае выведется "Meow" так как переменная animal хоть и является типом Animal но всё ещё хранит ссылку на объект типа Cat

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
    public override void makeSound()
    {
        Console.WriteLine("Meow");
    }
}   


