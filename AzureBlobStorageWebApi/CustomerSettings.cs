namespace AzureBlobStorageWebApi;

using System;
using System.Text.Json.Serialization;


public class CustomerSettings 
{
  internal object _ts;

  [JsonPropertyName("id")]
  public string Id { get; set; } // Unique identifier for the branding settings

  [JsonPropertyName("title")]
  public string Title { get; set; } 

  [JsonPropertyName("loginHeaderTitle")]
  public string LoginHeader { get; set; } 

  [JsonPropertyName("titlefontcolor")]
  public string TitleFontColor { get; set; } = "#ffffff"; 

  [JsonPropertyName("loginHeaderTitleColor")]
  public string LoginHeaderTitleColor { get; set; } = "#000"; 

  [JsonPropertyName("loginBody")]
  public string LoginBody { get; set; }  // Default value

  [JsonPropertyName("loginBodyColor")]
  public string LoginBodyColor { get; set; } = "#000"; // Default value 

  [JsonPropertyName("logoiconlink")]
  public string LogoIconLink { get; set; } = " "; // Default value

  [JsonPropertyName("backgroundimage")]
  public string BackgroundImage { get; set; }

  [JsonPropertyName("headerbarcolor")]
  public string HeaderBarColor { get; set; }  

  [JsonPropertyName("loginContainerColor")]
  public string LoginContainerColor { get; set; }  

  [JsonPropertyName("loginBTNName")]
  public string LoginBTNName { get; set; } 

  [JsonPropertyName("loginBTNColor")]
  public string LoginBTNColor { get; set; } = "#007bff"; 

  [JsonPropertyName("loginBTNTextColor")]
  public string LoginBTNTextColor { get; set; } = "#fff"; 

  [JsonPropertyName("loginBTNLogo")]
  public string LoginBTNLogo { get; set; } = " ";

  [JsonPropertyName("configureInitialMSG")]
  public string ConfigureInitialMSG { get; set; } 

  [JsonPropertyName("_ts")]
  public long Timestamp { get; set; }

  public string Partition => this.Id;

  public CustomerSettings()
  {
    this.Id = Guid.NewGuid().ToString();
    this.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
  }
}
