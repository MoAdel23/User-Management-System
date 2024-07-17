using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Eshop.Models;


//Email : ma7052012@gmail.com
//Password :  78*YfWQ.DQnU2.g



//Email : r3y6g040w@mozmail.com
//Password :  U.H9HpZvq_2zt+F



//Email : admin@yahoo.com
//Password :  Moadel2023*#
public class ApplicationUser : IdentityUser
{

    [Required , MaxLength(100)]
    public string FirstName { get; set; }

    [Required , MaxLength(100)]
    public string LastName { get; set; }

 
    public byte[]? ProfilePitcure { get; set; }
 }
