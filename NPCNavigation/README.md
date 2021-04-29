Prerequisites for the Array and NPC actions to work

- 1: In scene gameobject(s) with tagged, can be adjusted in actions what tag is, by default i use "Waypoint" and on empty gameobjects, these are the possible goto areas.
- 2: A Gameobject with the action WayPointArray (this finds and stores all waypoints found in scene) run only once when scene load, to improve performance.

- On a NPC just add the WaypointNPC and fill in info as needed, local NPC also needs a LocalVar that store the next waypoint.
