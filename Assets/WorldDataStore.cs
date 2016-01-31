using System;
using System.Collections.Generic;

public class WorldDataStore {

	public List<Town> _towns { get; private set; }
	
	public WorldDataStore () {
		_towns = new List<Town>();
	}

	public void storeTownsData(List<Town> towns) {
		_towns = towns;
	}
}

