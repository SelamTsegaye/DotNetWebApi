using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DotNetWebApi.Models;

/// <summary>
/// The location of one or more picnics.  Every picnic must have a location
/// </summary>
public class PicnicLocationReturn
{
    /// <summary>
    /// The Primary Key
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the location (must be unique)
    /// </summary>
    public string LocationName { get; set; } = null!;

    /// <summary>
    /// How many teddy bears can be accommodated at this location
    /// </summary>
    public int Capacity { get; set; }

    /// <summary>
    /// In what village, town or city is this location
    /// </summary>
    public string Municipality { get; set; } = null!;
    
    /// <summary>
    /// A collection of Picnic names for this location
    /// </summary>
    public IEnumerable<string> Picnics { get; init; } = null!;
}
