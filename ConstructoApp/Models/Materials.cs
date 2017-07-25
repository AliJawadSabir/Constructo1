using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ConstructoApp.Models
{
    public class Materials
    {
        [Required(ErrorMessage = "This field can not be empty.")]
        public double WallHeigth { set; get; }
        [Required(ErrorMessage = "This field can not be empty.")]
        public double WallLength { set; get; }
        [Required(ErrorMessage = "This field can not be empty.")]
        public double Wallthickness { get; set; }
        public double bricks { get; set; }
        public double sand { get; set; }
        public int[] numberMap { get; set; }
        public List<double> MapDim { get; set; }
        public double cement { get; set; }
        public double steel { get; set; }
        public double crush { get; set; }
        [Required(ErrorMessage = "This field can not be empty.")]
        public double RoomWidth { get; set; }
        [Required(ErrorMessage = "This field can not be empty.")]
        public double RoomLength { get; set; }
        [Required(ErrorMessage = "This field can not be empty.")]
        public double RoomHeight { get; set; }
        public double RoomHeigth { get; set; }
        public double Roomthickness { get; set; }
        public double Roofthickness { get; set; }
        public double RoomCount { set; get; }
        public double BathRoomWidth { set; get; }
        public double BathRoomHeight { set; get; }
        public double BathRoomLength { set; get; }
        public double BathRoomCount { set; get; }
        public double BathRoomthickness { set; get; }
        public double KitchenWidth { set; get; }
        public double KitchenHeight { set; get; }
        public double KitchenLength { set; get; }
        public double KitchenCount { set; get; }
        public double Kitchenthickness { set; get; }
        public int ratio { set; get; }
        public double DoorHeight { set; get; }
        public double DoorWidth { set; get; }
        public double KitchenDoorHeight { set; get; }
        public double KitchenDoorWidth { set; get; }
        public double BathDoorHeight { set; get; }
        public double BathDoorWidth { set; get; }
        public double LowPrice { set; get; }
        public double highPrice { set; get; }
        public double medimumPrice { set; get; }
        public double DoorLength { get; set; }
        public double WindowLength { get; set; }
        public double WindowWidth { get; set; }
        public List<double> Doors { get; set; }
        public List<double> Windows { get; set; }
    }
}