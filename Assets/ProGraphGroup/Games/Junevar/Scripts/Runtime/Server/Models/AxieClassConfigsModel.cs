using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ProGraphGroup.General.Models;
using UnityEngine;

namespace ProGraphGroup.Games.Junevar.Server.Models
{
    [Serializable]
    public class AxieClassConfigsModel
    {
        [JsonProperty("type")] public AxieClassType Type;
        [JsonProperty("stats")] public AxieStats Stats;

        [JsonProperty("strong_effect_target_classes")]
        public List<AxieClassType> StrongEffectTargetClasses = new List<AxieClassType>();

        [JsonProperty("weak_effect_target_classes")]
        public List<AxieClassType> WeakEffectTargetClasses = new List<AxieClassType>();

        [JsonProperty("image_url")] public string ImageUrl;
        [JsonProperty("color")] public List<SerializableColor> Colors = new List<SerializableColor>();

   
    }
}