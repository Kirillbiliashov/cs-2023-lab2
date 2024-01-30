using lab2;


Console.Write("Enter word length: ");

int removeLength;

while (!int.TryParse(Console.ReadLine(), out removeLength))
{
    Console.Write("Enter valid word length: ");
}

Console.Write("Enter text: ");

var text = Console.ReadLine();

new TextModifier(text, removeLength).RemoveWords();
