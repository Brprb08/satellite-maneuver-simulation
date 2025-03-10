# Satellite Maneuver Simulator Prototype  

An interactive, real-time N-body orbital physics simulator built in Unity, now enhanced with a C++ DLL for improved physics performance. This project features RK4 numerical integration, GPU-accelerated trajectory rendering, and real-time thrust mechanics.

[Watch the Demo Video](https://www.youtube.com/watch?v=aADKGJIdwKM) *Cntl-click to open in new tab*
![Orbit Mechanics Simulator in Track Cam](./Assets/Images/02-19Track.png)
![Satellite Close Up Elliptical Orbit](./Assets/Images/02-03SatelliteCloseUp.png)
![Simulator in Free Cam](./Assets/Images/02-03Free.png)


_Current state of the simulation. Top image shows the Track cam from a distance with current object you are tracking, showing orbit path, origin/ground path line, and apogee/perigee lines. The second image is Track cam up close showing the Satellite model. The third image shows Free cam where you can move around freely and place new satellites! Work in progress._

## Table of Contents
- [Overview](#overview)
- [Key Features](#key-features)
- [How It Works](#how-it-works)
- [How to Use](#how-to-use)
- [Planned Features](#planned-features)
- [Limitations](#limitations)
- [Getting Started](#getting-started)
- [Technical Physics Breakdown](./NBODY_PHYSICS_RK4.md) *(separate file for RK4 and gravity calculations)*

---

## Overview
This project is a real-time orbital mechanics simulator that allows users to visualize, manipulate, and experiment with accurate gravitational physics. It includes fully interactive thrust mechanics, trajectory predictions, and scalable time controls to demonstrate how small adjustments affect long-term orbits.

Built in Unity, it uses Runge-Kutta 4th Order (RK4) integration for accurate physics and GPU acceleration for smooth trajectory rendering.

---

## Key Features

### Full N-Body Orbital Simulation
- Each celestial body influences others using Newtonian gravity.
- Runge-Kutta 4th Order (RK4) integration ensures numerical stability over time.
- Earth remains stationary, but objects like the moon and satellites interact dynamically.

### C++ DLL Integration for Physics
- Core physics calculations are offloaded to a C++ DLL using native plugins.
- Significantly boosts performance by leveraging compiled code for heavy calculations.
- Reduces CPU overhead, improving simulation stability and frame rates.

### Real-Time GPU-Accelerated Trajectory Rendering
- Trajectory visualization is handled on the GPU, reducing CPU overhead.
- Predicted orbits update dynamically based on real-time inputs.

### Interactive Thrust Mechanics
- Apply prograde, retrograde, radial, and lateral burns to modify orbits.
- Mass-based force scaling for realistic physics.

### Multiple Camera Modes
- Track Camera: Follows a selected celestial body with velocity and altitude readouts.
- Free Camera: Roam the scene, place new objects, and analyze trajectories.

### Time Control and Scaling
- Adjust time scale from real-time to 100x.
- Pause and resume simulation without resetting orbits.

### Advanced Orbital Interactions
- Computes apogee and perigee dynamically.
- Supports orbital decay, close encounters, and gravity assists.

---

## How It Works  
*(For in-depth equations and derivations, see [NBody_Physics_RK4.md](./NBODY_PHYSICS_RK4.md))*

### Numerical Integration (RK4)
- Uses Runge-Kutta 4th Order (RK4) instead of Euler for better stability.
- Computes position and velocity updates using four derivative calculations per step.
- Prevents numerical drift, keeping orbits stable over long simulations.

### C++ Physics Module
- The core gravitational and RK4 physics logic is handled by a C++ DLL.
- Unity calls native C++ functions through the `NativePhysics` wrapper, enhancing performance.
- Avoids performance bottlenecks by processing large arrays and vector operations natively.

### Gravity Calculations
- Uses Newton’s Law of Gravitation to compute forces between all objects:
```
 F = G * (m1 * m2) / r^2 
```
- Avoids singularities by applying minimum distance thresholds.

---

## How to Use  

### Track Camera Mode
- Switch Tracked Object: Use the **dropdown menu** to select the object you want to track.
- Camera Controls: Right mouse button to rotate, scroll wheel to zoom.
- View Real-Time Data: Velocity (m/s and mph) and altitude (km and ft).
- **Earth Cam Button**: Toggles between two camera modes:
  - **Earth Cam**: Centers the view on Earth, making it easier to observe satellite orbits around the planet.
  - **Satellite Cam**: Centers the view on the selected satellite, following it as it moves around Earth.

### Free Camera Mode
- Move with `WASD` or arrow keys, rotate with right-click, zoom with scroll wheel.
- Place New Planets: Set mass, radius, and velocity dynamically.

### Thrust Controls
- `Prograde Burn ↑` - Increase orbital speed.
- `Retrograde Burn ↓` - Decrease orbital speed.
- `Radial In/Out` - Modify altitude.
- `Lateral Burns` - Change inclination.

### Time Scaling
- Adjust with the slider (1x to 100x).
- Press `R` to reset time to normal speed.

---

## Planned Features  

- Maneuver Planning System - Pre-plan orbital burns similar to Kerbal Space Program.
- Full N-Body Simulation (Moving Earth) - Allow Earth to respond dynamically to forces.
- Barnes-Hut Optimization - Improve N-body calculations for better performance.
- Fuel System and Delta-V Calculations - Limit thrust by fuel mass, making burns more strategic.

---

## Limitations
- **No Aerodynamic Effects**: Currently, there’s no atmosphere or drag modeling.
- **No Relativistic Corrections**: Strictly Newtonian physics—relativistic effects are not accounted for.
- **Simplified Collisions**: Bodies are removed rather than merged; no physical collision response.
- **Prototype Thrust**: Thrust controls are still basic. More detailed burn planning is not yet implemented.

## Status

The core physics engine now utilizes a C++ DLL to improve computational performance and stability. Visual improvements and advanced feature development are ongoing.

## Getting Started

### Prerequisites

- **Unity:** Ensure you have Unity installed (version 2020.3 or later recommended).
- **Git:** For version control and cloning the repository.

### Installation

1. **Clone the Repository:**

- HTTPS:
  ```bash
  git clone https://github.com/Brprb08/space-orbit-simulation.git
  ```
- SSH:
  ```
  git clone git@github.com:Brprb08/space-orbit-simulation.git
  ```
- Github CLI:
  ```
  gh repo clone Brprb08/space-orbit-simulation
  ```

2. **Open in Unity:**

- Launch Unity Hub.
- Click on `Add` and navigate to the cloned repository folder.
- Open the project.

2. **Run the Simulation:**

- Open the `OrbitSimulation.unity` file located in the `Assets/Scenes` directory.
- If no hierarchy or GameObjects are visible, ensure you have opened the correct scene by double-clicking `OrbitSimulation.unity`.
- Click the `Play` button to start the simulation.

[⬆ Back to Top](#satellite-maneuver-simulator-prototype)
