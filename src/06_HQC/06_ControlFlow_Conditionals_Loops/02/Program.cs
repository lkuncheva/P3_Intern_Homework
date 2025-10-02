// First code snippet
var potato = GetPotato();

if (potato != null&& potato.IsPeeled && !potato.IsRotten)
{ 
    Cook(potato);
}

// Second code snippet
bool isWithinXBounds = x >= MIN_X && x <= MAX_X;
bool isWithinYBounds = y >= MIN_Y && y <= MAX_Y;

if (isWithinXBounds && isWithinYBounds && canVisitCell)
{
    VisitCell();
}

// Or this --->
/*
if (x >= MIN_X && x <= MAX_X &&
    y >= MIN_Y && y <= MAX_Y &&
    canVisitCell)
{
    VisitCell();
}
*/