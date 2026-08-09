# Project Twilight

Project Twilight is a **raising-sim game** inspired by the *Princess Maker* series and other games such as *Long Live the Queen*.

The goal is to combine the stat-management and scheduling systems of a traditional raising sim with a stronger **visual-novel/event-driven structure**, while removing adventuring and keeping the overall game extremely lightweight.

## Technical Goals

Project Twilight is being designed around a small, efficient core rather than relying heavily on modern engine features.

Some of the systems currently being developed include:

* Custom fixed-point mathematics
* Custom random number generation
* Custom square-root, exponent, and division functions
* Bit-packed game data
* Custom save-file format
* Event/opcode interpreter
* Binary dialogue and event data
* Lightweight job/activity system
* Custom image format with indexed palettes
* Data-driven events and activities

The architecture is intentionally designed so that most game content can be added without changing the underlying engine.
Once the core systems are complete, adding jobs, events, dialogue, activities, and other content should primarily involve creating data rather than rewriting engine code.

## Portability

The project is currently being developed in **C# and Unity**, but the long-term goal is to make the core systems portable to **C89/C** 
with platform-specific graphics, audio, input, and filesystem layers.

This is being designed with potential ports to older hardware in mind, including systems such as the **Atari Jaguar**, rather than requiring modern hardware.

The emphasis is therefore on:

**small code → small data → low memory usage → low computational cost → easy portability**

The project is still actively in development, and many of these systems are unfinished or being refined.

