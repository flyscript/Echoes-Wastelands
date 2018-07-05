/*
  Pages Items
  
  Will Chapman
  Callum yates
  05/07/2018
  
  Pages is an class that inherits from Item, and sets itself up so that many different pages can be made
  
  This is just an example to be built heavily upon by Callum
  
*/

public abstract class Page : Item
{

    protected Page(int _pageNumber, string _description)
        : base("Page" + _pageNumber.ToString(), _description, ItemType.Collectables)
    {
        
    }
        
}

/*
In code somewhere:

Page page1 = new Page(1, "Page 1 of 10 of the runes");

*/