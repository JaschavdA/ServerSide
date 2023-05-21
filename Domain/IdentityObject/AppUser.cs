
using Microsoft.AspNetCore.Identity;

namespace Bordspelavond.IdentityObject;

// Add profile data for application users by adding properties to the AppUser class
public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDay { get; set; }

    public bool IsEighteen()
    {
        return GetAge() >= 18;
    }

    public bool IsSixteen()
    {
        return GetAge() >= 16;
    }

    private int GetAge()
    {
        //Found at https://stackoverflow.com/questions/9/how-do-i-calculate-someones-age-based-on-a-datetime-type-birthday
        var today = DateTime.Today;
        var age = today.Year - BirthDay.Year;
        if (BirthDay.Date > today.AddYears(-age)) age--;
        return age;
    }
}



