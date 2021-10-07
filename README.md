# TransformStructureVisualiser

Add this monobehaviour to a game object to preview connectivity between this object and all of its children in the scene view, works like the BoneRenderer class from Unity Animations.Rigging API, but is fully self-contained and has some graphics tuning options.

Note: If you are interested in how the code works, these two lines look weird:

Vector3 i = new Vector3(k.z, k.z, -k.x-k.y).normalized;

if (i.magnitude == 0) i = new Vector3(-k.y-k.z, k.x, k.x).normalized;

They work because in Unity normalizing a near zero vector gives a zero vector. And floating point comparisons to zero (normally a bad idea) will return true if the floating point value is "small". There is no general solution for building a cartesian system between two points, you have to try the two methods and use the non-zero one.
