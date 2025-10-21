public class Chef
{
    public void Cook()
    {
        var potato = GetPotato();
        var carrot = GetCarrot();
        var bowl = GetBowl();

        PrepareVegetable(potato);
        PrepareVegetable(carrot);

        bowl.Add(carrot);
        bowl.Add(potato);
    }

    private void PrepareVegetable(Vegetable vegetable)
    {
        Peel(vegetable);
        Cut(vegetable);
    }

    private void Cut(Vegetable potato)
    {
        //...
    }

    private void Peel(Vegetable potato)
    {
        //...
    }

    private Bowl GetBowl()
    {
        //... 
        return new Bowl();
    }

    private Carrot GetCarrot()
    {
        //...
        return new Carrot();
    }

    private Potato GetPotato()
    {
        //...
        return new Potato();
    }
}