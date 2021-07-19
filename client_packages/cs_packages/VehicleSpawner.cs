using System;
using System.Collections.Generic;
using RAGE;
using RAGE.NUI;

namespace VehicleSpawner
{
    public class VehicleSpawner : Events.Script
    {
        public VehicleSpawner()
        {
            Events.Add("vSpawner", vSpawner);
        }
        public void vSpawner(object[] args)
        {
            string vehicleSelected = null;
            bool sitIn = false;

            RAGE.Ui.Cursor.Visible = true;
            Chat.Show(false);

            UIMenu vMenu = new UIMenu("Fahrzeug Spawner", "Spawn dein eigenes Fahrzeug!");

            MenuPool menuPool = new MenuPool();
            menuPool.Add(vMenu);

            UIMenuCheckboxItem SitInCar = new UIMenuCheckboxItem("Ins Fahrzeug setzen?", sitIn);
            vMenu.AddItem(SitInCar);

            var vehicleNames = new List<dynamic>
            {
                "Sultan",
                "Tropos",
                "Surano",
                "zr3803",
                "Mamba"
            };

            vehicleSelected = vehicleNames[0];

            UIMenuListItem selectedVehicleNames = new UIMenuListItem("Fahrzeuge", vehicleNames, 0);
            vMenu.AddItem(selectedVehicleNames);

            UIMenuItem spawnButton = new UIMenuItem("Spawn");
            vMenu.AddItem(spawnButton);

            vMenu.OnMenuClose += (menu) =>
            {
                if(menu == vMenu)
                {
                    RAGE.Ui.Cursor.Visible = false;
                    Chat.Show(true);
                    vMenu.Visible = false;
                    vMenu.FreezeAllInput = false;
                }
            };

            vMenu.OnCheckboxChange += (menu, item, flag) =>
            {
                if(menu == vMenu && item == SitInCar)
                {
                    sitIn = flag;
                }
            };

            vMenu.OnListChange += (menu, item, index) =>
            {
                if(menu == vMenu && item == selectedVehicleNames)
                {
                    vehicleSelected = item.IndexToItem(index).ToString();
                }
            };

            spawnButton.Activated += (menu, item) =>
            {
                if(menu == vMenu && item == spawnButton)
                {
                    RAGE.Ui.Cursor.Visible = false;
                    Chat.Show(true);
                    vMenu.Visible = false;
                    vMenu.FreezeAllInput = false;
                    Events.CallRemote("SpawnVehicleServer", vehicleSelected, sitIn);
                }
            };

            vMenu.Visible = true;
            vMenu.FreezeAllInput = true;

            vMenu.RefreshIndex();

            Events.Tick += (name) =>
            {
                menuPool.ProcessMenus();
            };

        }
    }
}
