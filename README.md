# ZooWorld

## Description
Simple top-down 3D simulation with prey and predators.

- Prey (Frog): jump movement
- Predator (Snake): linear movement
- Food chain interactions
- UI counters for dead animals

## Controls
No player controls. Simulation runs automatically.

## Architecture
- Animal data stored in ScriptableObjects
- Movement implemented via strategy pattern
- Centralized collision handling
- Decoupled UI with event-driven updates

## Notes
Project is structured to allow easy addition of new animal types and behaviors.
