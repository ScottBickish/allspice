namespace allspice.Models
{
  public class Ingredient
  {
    public string IngredientName {get; set;}
    public string Quantity {get; set;}
    public int RecipeId {get; set;}
    public string CreatorId { get; set; }
    public int Id { get; set; }
  }
}