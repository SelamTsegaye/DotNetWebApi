using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DotNetWebApi.Models;

public partial class TeddyBearReturn
{
    /// <summary>
    /// The Primary Key
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The Teddy Bear&apos;s name.  Each is unique
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// All teddy bears have a primary color.  The color is a string (but should be picked from a list)
    /// </summary>
    public string PrimaryColor { get; set; } = null!;

    /// <summary>
    /// Teddy Bears may have a secondary color.  The color is a string (but should be picked from a list)
    /// </summary>
    public string? AccentColor { get; set; }

    /// <summary>
    /// Is the Teddy Bear dressed (true or false)
    /// </summary>
    public bool? IsDressed { get; set; }

    /// <summary>
    /// Who is the teddy bear&apos;s owner
    /// </summary>
    public string OwnerName { get; set; } = null!;

    /// <summary>
    /// Teddy bears may have a defining characteristic - fluffy, polite, whatever
    /// </summary>
    public string? Characteristic { get; set; }

    /// <summary>
    /// A collection of Picnic names for this teddy bear
    /// </summary>
    public IEnumerable<string> Picnics { get; init; } = null!;
}
