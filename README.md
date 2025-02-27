# Hyperleap Game Design Architecture

Building on the technical foundation, here's an overview of Hyperleap's game design architecture:

## Core Gameplay Loop
- Exploration-based platforming through carefully designed levels
- Player progression through collecting coins and power-ups
- Overcoming obstacles and defeating enemies
- Level completion goals (likely reaching an end point or collecting key items)

## Player Mechanics
- Jump-based platforming as the primary interaction
- Power-up system that appears to enhance player abilities
- Health management system for survival
- Possible special abilities or transformations (suggested by the Mushroom power-up)

## Level Design Framework
- Tile-based construction with platform variations
- Environmental hazards and obstacles
- Secret areas and collectibles to reward exploration
- Vertical and horizontal traversal challenges

## Enemy Design
- Basic enemies with simple movement patterns (like Goomba)
- Possible more complex enemy types with unique behaviors
- Enemy health system suggesting combat mechanics

## Progression System
- Score tracking (visible in GameScore.asset)
- Coin collection as a core reward mechanism
- Level-to-level advancement structure

## User Experience
- Main menu to game flow
- Loading screen between major transitions
- UI system for displaying player status and progress

## Visual Language
- Consistent pixel art aesthetics
- Character design that supports gameplay mechanics
- Environmental storytelling through visual elements
- Animated elements to enhance game feel

The architecture follows a classic platformer design with modern enhancements, creating a familiar yet fresh experience. The structure suggests a game that values both accessibility for new players and depth for platformer enthusiasts.

# Event Bus & Finite State Machine Implementation Potential for Hyperleap

Implementing a Scriptable Object (SO) event bus coupled with a Finite State Machine (FSM) for mob behavior would significantly enhance Hyperleap's architecture. Here's how this lab could benefit the game:

## Moving Forward: SO Architecture

### Benefits
- **Decoupled Communication**: Game entities could interact without direct references, reducing dependencies
- **Centralized Event Management**: All game events (player actions, collisions, power-up activations) flow through a managed system
- **Improved Testability**: Events can be triggered manually for testing specific scenarios

### Implementation Approach
- Create base ScriptableObject event types (ValueChangedEvent, TriggerEvent, etc.)
- Design specialized event SOs for game-specific needs (PlayerDamagedEvent, PowerUpCollectedEvent)
- Implement listener registration/unregistration for game objects
- Add debugging tools to monitor event flow during development

## Finite State Machine for Mobs

### Benefits
- **Organized Behavior Logic**: Each enemy type can have clearly defined states (Patrol, Chase, Attack, Stunned)
- **Extensibility**: New enemy behaviors become modular additions rather than code rewrites
- **Predictable Interactions**: Game designers can tune each state independently

### Implementation Approach
- Create a base State class and StateMachine controller
- Define state transitions based on game events and environmental conditions
- Implement specific states for different enemy types:
  - Basic enemies: Patrol and Attack states
  - More complex enemies: Multiple attack patterns, retreat behaviors
  - Boss characters: Phase-based state progression

## Integration Between Systems

The real power comes from connecting these systems:
- Enemy state transitions triggered by events from the bus (PlayerNearbyEvent → Chase state)
- States emitting events to the bus (AttackState → DamageDealtEvent)
- Game manager monitoring state changes via events for gameplay progression

## Technical Architecture
```
EventBus/
  ├── EventBase.cs (Base SO for all events)
  ├── GameEvents/ (Specific event types)
  │   ├── PlayerEvents.cs
  │   ├── EnemyEvents.cs
  │   └── LevelEvents.cs
  └── EventListener.cs (Component for objects that react to events)

StateMachine/
  ├── State.cs (Base state class)
  ├── StateMachine.cs (Controller for state transitions)
  └── EnemyStates/
      ├── PatrolState.cs
      ├── ChaseState.cs
      ├── AttackState.cs
      └── StunnedState.cs
```

This lab would transform the current direct-reference approach in scripts like `EnemyController.cs` and `Goomba.cs` into a more robust, maintainable system that could handle increasingly complex mob behaviors as the game evolves.