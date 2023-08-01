using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace KyoPro
{
public static class CONST
{
    public const long MOD = 998244353;
}

public static class EntryPoint {
    public static void Main() {
        var solver = new Solver();
        solver.Solve();
    }
}

public class Solver {
    public void Solve()
    {
        var N = Rl();
        var X1Y1Z1X2Y2Z2s = new List<long[]>();
        for (int i = 0; i < N; i++)
        {
            var X1Y1Z1X2Y2Z2 = Rla();
            X1Y1Z1X2Y2Z2s.Add(X1Y1Z1X2Y2Z2);
        }


    }

    static string Rs(){return Console.ReadLine();}
    static int Ri(){return int.Parse(Console.ReadLine() ?? string.Empty);}
    static long Rl(){return long.Parse(Console.ReadLine() ?? string.Empty);}
    static double Rd(){return double.Parse(Console.ReadLine() ?? string.Empty);}
    static BigInteger Rb(){return BigInteger.Parse(Console.ReadLine() ?? string.Empty);}
    static string[] Rsa(char sep=' '){return Console.ReadLine().Split(sep);}
    static int[] Ria(char sep=' '){return Array.ConvertAll(Console.ReadLine().Split(sep),int.Parse);}
    static long[] Rla(char sep=' '){return Array.ConvertAll(Console.ReadLine().Split(sep),long.Parse);}
    static double[] Rda(char sep=' '){return Array.ConvertAll(Console.ReadLine().Split(sep),double.Parse);}
    static BigInteger[] Rba(char sep=' '){return Array.ConvertAll(Console.ReadLine().Split(sep),BigInteger.Parse);}
    static int[] GenerateNums(int num, int N){return Enumerable.Repeat(num, N).ToArray();}
    static int[] GenerateSuretsu(int N){return Enumerable.Range(0, N).ToArray();}
}

public class Vector3
{
	public long x;
	public long y;
	public long z;
	
	public Vector3(long x, long y, long z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	// +の演算子をオーバーロード
	public static Vector3 operator +(Vector3 a, Vector3 b)
	{
		return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
	}
}

// public class Bounds
// {
// 	public Vector3 center;
// 	public Vector3 size;
// 	public Vector3 min;
// 	public Vector3 max;
//
// 	public Bounds(Vector3 center, Vector3 size)
// 	{
// 		this.center = center;
// 		this.size = size;
// 		min = new Vector3(center.x - size.x / 2, center.y - size.y / 2, center.z - size.z / 2);
// 		max = new Vector3(center.x + size.x / 2, center.y + size.y / 2, center.z + size.z / 2);
// 	}
//
// 	public bool Intersects(Bounds checkBounds)
// 	{
// 		return (min.x <= checkBounds.max.x && max.x >= checkBounds.min.x) &&
// 			(min.y <= checkBounds.max.y && max.y >= checkBounds.min.y) &&
// 			(min.z <= checkBounds.max.z && max.z >= checkBounds.min.z);
// 	}
// }
//
// // A node in a BoundsOctree
// // Copyright 2014 Nition, BSD licence (see LICENCE file). www.momentstudio.co.nz
// public class BoundsOctreeNode<T> {
// 	// Centre of this node
// 	public Vector3 Center { get; private set; }
//
// 	// Length of this node if it has a looseness of 1.0
// 	public long BaseLength { get; private set; }
//
// 	// Looseness value for this node
// 	long looseness;
//
// 	// Minimum size for a node in this octree
// 	long minSize;
//
// 	// Actual length of sides, taking the looseness value into account
// 	long adjLength;
//
// 	// Bounding box that represents this node
// 	Bounds bounds = default(Bounds);
//
// 	// Objects in this node
// 	readonly List<OctreeObject> objects = new List<OctreeObject>();
//
// 	// Child nodes, if any
// 	BoundsOctreeNode<T>[] children = null;
//
// 	bool HasChildren { get { return children != null; } }
//
// 	// Bounds of potential children to this node. These are actual size (with looseness taken into account), not base size
// 	Bounds[] childBounds;
//
// 	// If there are already NUM_OBJECTS_ALLOWED in a node, we split it into children
// 	// A generally good number seems to be something around 8-15
// 	const int NUM_OBJECTS_ALLOWED = 8;
//
// 	// An object in the octree
// 	struct OctreeObject {
// 		public T Obj;
// 		public Bounds Bounds;
// 	}
//
// 	/// <summary>
// 	/// Constructor.
// 	/// </summary>
// 	/// <param name="baseLengthVal">Length of this node, not taking looseness into account.</param>
// 	/// <param name="minSizeVal">Minimum size of nodes in this octree.</param>
// 	/// <param name="loosenessVal">Multiplier for baseLengthVal to get the actual size.</param>
// 	/// <param name="centerVal">Centre position of this node.</param>
// 	public BoundsOctreeNode(long baseLengthVal, long minSizeVal, long loosenessVal, Vector3 centerVal) {
// 		SetValues(baseLengthVal, minSizeVal, loosenessVal, centerVal);
// 	}
//
// 	// #### PUBLIC METHODS ####
//
// 	/// <summary>
// 	/// Add an object.
// 	/// </summary>
// 	/// <param name="obj">Object to add.</param>
// 	/// <param name="objBounds">3D bounding box around the object.</param>
// 	/// <returns>True if the object fits entirely within this node.</returns>
// 	public bool Add(T obj, Bounds objBounds) {
// 		if (!Encapsulates(bounds, objBounds)) {
// 			return false;
// 		}
// 		SubAdd(obj, objBounds);
// 		return true;
// 	}
//
// 	/// <summary>
// 	/// Remove an object. Makes the assumption that the object only exists once in the tree.
// 	/// </summary>
// 	/// <param name="obj">Object to remove.</param>
// 	/// <returns>True if the object was removed successfully.</returns>
// 	public bool Remove(T obj) {
// 		bool removed = false;
//
// 		for (int i = 0; i < objects.Count; i++) {
// 			if (objects[i].Obj.Equals(obj)) {
// 				removed = objects.Remove(objects[i]);
// 				break;
// 			}
// 		}
//
// 		if (!removed && children != null) {
// 			for (int i = 0; i < 8; i++) {
// 				removed = children[i].Remove(obj);
// 				if (removed) break;
// 			}
// 		}
//
// 		if (removed && children != null) {
// 			// Check if we should merge nodes now that we've removed an item
// 			if (ShouldMerge()) {
// 				Merge();
// 			}
// 		}
//
// 		return removed;
// 	}
//
// 	/// <summary>
// 	/// Removes the specified object at the given position. Makes the assumption that the object only exists once in the tree.
// 	/// </summary>
// 	/// <param name="obj">Object to remove.</param>
// 	/// <param name="objBounds">3D bounding box around the object.</param>
// 	/// <returns>True if the object was removed successfully.</returns>
// 	public bool Remove(T obj, Bounds objBounds) {
// 		if (!Encapsulates(bounds, objBounds)) {
// 			return false;
// 		}
// 		return SubRemove(obj, objBounds);
// 	}
//
// 	/// <summary>
// 	/// Check if the specified bounds intersect with anything in the tree. See also: GetColliding.
// 	/// </summary>
// 	/// <param name="checkBounds">Bounds to check.</param>
// 	/// <returns>True if there was a collision.</returns>
// 	public bool IsColliding(ref Bounds checkBounds) {
// 		// Are the input bounds at least partially in this node?
// 		if (!bounds.Intersects(checkBounds)) {
// 			return false;
// 		}
//
// 		// Check against any objects in this node
// 		for (int i = 0; i < objects.Count; i++) {
// 			if (objects[i].Bounds.Intersects(checkBounds)) {
// 				return true;
// 			}
// 		}
//
// 		// Check children
// 		if (children != null) {
// 			for (int i = 0; i < 8; i++) {
// 				if (children[i].IsColliding(ref checkBounds)) {
// 					return true;
// 				}
// 			}
// 		}
//
// 		return false;
// 	}
//
// 	/// <summary>
// 	/// Returns an array of objects that intersect with the specified bounds, if any. Otherwise returns an empty array. See also: IsColliding.
// 	/// </summary>
// 	/// <param name="checkBounds">Bounds to check. Passing by ref as it improves performance with structs.</param>
// 	/// <param name="result">List result.</param>
// 	/// <returns>Objects that intersect with the specified bounds.</returns>
// 	public void GetColliding(ref Bounds checkBounds, List<T> result) {
// 		// Are the input bounds at least partially in this node?
// 		if (!bounds.Intersects(checkBounds)) {
// 			return;
// 		}
//
// 		// Check against any objects in this node
// 		for (int i = 0; i < objects.Count; i++) {
// 			if (objects[i].Bounds.Intersects(checkBounds)) {
// 				result.Add(objects[i].Obj);
// 			}
// 		}
//
// 		// Check children
// 		if (children != null) {
// 			for (int i = 0; i < 8; i++) {
// 				children[i].GetColliding(ref checkBounds, result);
// 			}
// 		}
// 	}
// 	/// <summary>
// 	/// Set the 8 children of this octree.
// 	/// </summary>
// 	/// <param name="childOctrees">The 8 new child nodes.</param>
// 	public void SetChildren(BoundsOctreeNode<T>[] childOctrees) {
// 		if (childOctrees.Length != 8) {
// 			throw new Exception("Child octree array must be length 8. Was length: " + childOctrees.Length);
// 		}
//
// 		children = childOctrees;
// 	}
//
// 	public Bounds GetBounds() {
// 		return bounds;
// 	}
//
// 	/// <summary>
// 	/// We can shrink the octree if:
// 	/// - This node is >= double minLength in length
// 	/// - All objects in the root node are within one octant
// 	/// - This node doesn't have children, or does but 7/8 children are empty
// 	/// We can also shrink it if there are no objects left at all!
// 	/// </summary>
// 	/// <param name="minLength">Minimum dimensions of a node in this octree.</param>
// 	/// <returns>The new root, or the existing one if we didn't shrink.</returns>
// 	public BoundsOctreeNode<T> ShrinkIfPossible(long minLength) {
// 		if (BaseLength < (2 * minLength)) {
// 			return this;
// 		}
// 		if (objects.Count == 0 && (children == null || children.Length == 0)) {
// 			return this;
// 		}
//
// 		// Check objects in root
// 		int bestFit = -1;
// 		for (int i = 0; i < objects.Count; i++) {
// 			OctreeObject curObj = objects[i];
// 			int newBestFit = BestFitChild(curObj.Bounds.center);
// 			if (i == 0 || newBestFit == bestFit) {
// 				// In same octant as the other(s). Does it fit completely inside that octant?
// 				if (Encapsulates(childBounds[newBestFit], curObj.Bounds)) {
// 					if (bestFit < 0) {
// 						bestFit = newBestFit;
// 					}
// 				}
// 				else {
// 					// Nope, so we can't reduce. Otherwise we continue
// 					return this;
// 				}
// 			}
// 			else {
// 				return this; // Can't reduce - objects fit in different octants
// 			}
// 		}
//
// 		// Check objects in children if there are any
// 		if (children != null) {
// 			bool childHadContent = false;
// 			for (int i = 0; i < children.Length; i++) {
// 				if (children[i].HasAnyObjects()) {
// 					if (childHadContent) {
// 						return this; // Can't shrink - another child had content already
// 					}
// 					if (bestFit >= 0 && bestFit != i) {
// 						return this; // Can't reduce - objects in root are in a different octant to objects in child
// 					}
// 					childHadContent = true;
// 					bestFit = i;
// 				}
// 			}
// 		}
//
// 		// Can reduce
// 		if (children == null) {
// 			// We don't have any children, so just shrink this node to the new size
// 			// We already know that everything will still fit in it
// 			SetValues(BaseLength / 2, minSize, looseness, childBounds[bestFit].center);
// 			return this;
// 		}
//
// 		// No objects in entire octree
// 		if (bestFit == -1) {
// 			return this;
// 		}
//
// 		// We have children. Use the appropriate child as the new root node
// 		return children[bestFit];
// 	}
//
// 	/// <summary>
// 	/// Find which child node this object would be most likely to fit in.
// 	/// </summary>
// 	/// <param name="objBounds">The object's bounds.</param>
// 	/// <returns>One of the eight child octants.</returns>
// 	public int BestFitChild(Vector3 objBoundsCenter) {
// 		return (objBoundsCenter.x <= Center.x ? 0 : 1) + (objBoundsCenter.y >= Center.y ? 0 : 4) + (objBoundsCenter.z <= Center.z ? 0 : 2);
// 	}
//
//     /// <summary>
//     /// Checks if this node or anything below it has something in it.
//     /// </summary>
//     /// <returns>True if this node or any of its children, grandchildren etc have something in them</returns>
//     public bool HasAnyObjects() {
//         if (objects.Count > 0) return true;
//
//         if (children != null) {
//             for (int i = 0; i < 8; i++) {
//                 if (children[i].HasAnyObjects()) return true;
//             }
//         }
//
//         return false;
//     }
//
//     /*
// 	/// <summary>
// 	/// Get the total amount of objects in this node and all its children, grandchildren etc. Useful for debugging.
// 	/// </summary>
// 	/// <param name="startingNum">Used by recursive calls to add to the previous total.</param>
// 	/// <returns>Total objects in this node and its children, grandchildren etc.</returns>
// 	public int GetTotalObjects(int startingNum = 0) {
// 		int totalObjects = startingNum + objects.Count;
// 		if (children != null) {
// 			for (int i = 0; i < 8; i++) {
// 				totalObjects += children[i].GetTotalObjects();
// 			}
// 		}
// 		return totalObjects;
// 	}
// 	*/
//
//     // #### PRIVATE METHODS ####
//
//     /// <summary>
//     /// Set values for this node.
//     /// </summary>
//     /// <param name="baseLengthVal">Length of this node, not taking looseness into account.</param>
//     /// <param name="minSizeVal">Minimum size of nodes in this octree.</param>
//     /// <param name="loosenessVal">Multiplier for baseLengthVal to get the actual size.</param>
//     /// <param name="centerVal">Centre position of this node.</param>
//     void SetValues(long baseLengthVal, long minSizeVal, long loosenessVal, Vector3 centerVal) {
// 		BaseLength = baseLengthVal;
// 		minSize = minSizeVal;
// 		looseness = loosenessVal;
// 		Center = centerVal;
// 		adjLength = looseness * baseLengthVal;
//
// 		// Create the bounding box.
// 		Vector3 size = new Vector3(adjLength, adjLength, adjLength);
// 		bounds = new Bounds(Center, size);
//
// 		long quarter = BaseLength / 4;
// 		long childActualLength = (BaseLength / 2) * looseness;
// 		Vector3 childActualSize = new Vector3(childActualLength, childActualLength, childActualLength);
// 		childBounds = new Bounds[8];
// 		childBounds[0] = new Bounds(Center + new Vector3(-quarter, quarter, -quarter), childActualSize);
// 		childBounds[1] = new Bounds(Center + new Vector3(quarter, quarter, -quarter), childActualSize);
// 		childBounds[2] = new Bounds(Center + new Vector3(-quarter, quarter, quarter), childActualSize);
// 		childBounds[3] = new Bounds(Center + new Vector3(quarter, quarter, quarter), childActualSize);
// 		childBounds[4] = new Bounds(Center + new Vector3(-quarter, -quarter, -quarter), childActualSize);
// 		childBounds[5] = new Bounds(Center + new Vector3(quarter, -quarter, -quarter), childActualSize);
// 		childBounds[6] = new Bounds(Center + new Vector3(-quarter, -quarter, quarter), childActualSize);
// 		childBounds[7] = new Bounds(Center + new Vector3(quarter, -quarter, quarter), childActualSize);
// 	}
//
// 	/// <summary>
// 	/// Private counterpart to the public Add method.
// 	/// </summary>
// 	/// <param name="obj">Object to add.</param>
// 	/// <param name="objBounds">3D bounding box around the object.</param>
// 	void SubAdd(T obj, Bounds objBounds) {
// 		// We know it fits at this level if we've got this far
//
// 		// We always put things in the deepest possible child
// 		// So we can skip some checks if there are children aleady
// 		if (!HasChildren) {
// 			// Just add if few objects are here, or children would be below min size
// 			if (objects.Count < NUM_OBJECTS_ALLOWED || (BaseLength / 2) < minSize) {
// 				OctreeObject newObj = new OctreeObject { Obj = obj, Bounds = objBounds };
// 				objects.Add(newObj);
// 				return; // We're done. No children yet
// 			}
//
// 			// Fits at this level, but we can go deeper. Would it fit there?
// 			// Create the 8 children
// 			int bestFitChild;
// 			if (children == null) {
// 				Split();
// 				if (children == null) {
// 					throw new Exception("Child creation failed for an unknown reason. Early exit.");
// 				}
//
// 				// Now that we have the new children, see if this node's existing objects would fit there
// 				for (int i = objects.Count - 1; i >= 0; i--) {
// 					OctreeObject existingObj = objects[i];
// 					// Find which child the object is closest to based on where the
// 					// object's center is located in relation to the octree's center
// 					bestFitChild = BestFitChild(existingObj.Bounds.center);
// 					// Does it fit?
// 					if (Encapsulates(children[bestFitChild].bounds, existingObj.Bounds)) {
// 						children[bestFitChild].SubAdd(existingObj.Obj, existingObj.Bounds); // Go a level deeper
// 						objects.Remove(existingObj); // Remove from here
// 					}
// 				}
// 			}
// 		}
//
// 		// Handle the new object we're adding now
// 		int bestFit = BestFitChild(objBounds.center);
// 		if (Encapsulates(children[bestFit].bounds, objBounds)) {
// 			children[bestFit].SubAdd(obj, objBounds);
// 		}
// 		else {
// 			// Didn't fit in a child. We'll have to it to this node instead
// 			OctreeObject newObj = new OctreeObject { Obj = obj, Bounds = objBounds };
// 			objects.Add(newObj);
// 		}
// 	}
//
// 	/// <summary>
// 	/// Private counterpart to the public <see cref="Remove(T, Bounds)"/> method.
// 	/// </summary>
// 	/// <param name="obj">Object to remove.</param>
// 	/// <param name="objBounds">3D bounding box around the object.</param>
// 	/// <returns>True if the object was removed successfully.</returns>
// 	bool SubRemove(T obj, Bounds objBounds) {
// 		bool removed = false;
//
// 		for (int i = 0; i < objects.Count; i++) {
// 			if (objects[i].Obj.Equals(obj)) {
// 				removed = objects.Remove(objects[i]);
// 				break;
// 			}
// 		}
//
// 		if (!removed && children != null) {
// 			int bestFitChild = BestFitChild(objBounds.center);
// 			removed = children[bestFitChild].SubRemove(obj, objBounds);
// 		}
//
// 		if (removed && children != null) {
// 			// Check if we should merge nodes now that we've removed an item
// 			if (ShouldMerge()) {
// 				Merge();
// 			}
// 		}
//
// 		return removed;
// 	}
//
// 	/// <summary>
// 	/// Splits the octree into eight children.
// 	/// </summary>
// 	void Split() {
// 		long quarter = BaseLength / 4f;
// 		long newLength = BaseLength / 2;
// 		children = new BoundsOctreeNode<T>[8];
// 		children[0] = new BoundsOctreeNode<T>(newLength, minSize, looseness, Center + new Vector3(-quarter, quarter, -quarter));
// 		children[1] = new BoundsOctreeNode<T>(newLength, minSize, looseness, Center + new Vector3(quarter, quarter, -quarter));
// 		children[2] = new BoundsOctreeNode<T>(newLength, minSize, looseness, Center + new Vector3(-quarter, quarter, quarter));
// 		children[3] = new BoundsOctreeNode<T>(newLength, minSize, looseness, Center + new Vector3(quarter, quarter, quarter));
// 		children[4] = new BoundsOctreeNode<T>(newLength, minSize, looseness, Center + new Vector3(-quarter, -quarter, -quarter));
// 		children[5] = new BoundsOctreeNode<T>(newLength, minSize, looseness, Center + new Vector3(quarter, -quarter, -quarter));
// 		children[6] = new BoundsOctreeNode<T>(newLength, minSize, looseness, Center + new Vector3(-quarter, -quarter, quarter));
// 		children[7] = new BoundsOctreeNode<T>(newLength, minSize, looseness, Center + new Vector3(quarter, -quarter, quarter));
// 	}
//
// 	/// <summary>
// 	/// Merge all children into this node - the opposite of Split.
// 	/// Note: We only have to check one level down since a merge will never happen if the children already have children,
// 	/// since THAT won't happen unless there are already too many objects to merge.
// 	/// </summary>
// 	void Merge() {
// 		// Note: We know children != null or we wouldn't be merging
// 		for (int i = 0; i < 8; i++) {
// 			BoundsOctreeNode<T> curChild = children[i];
// 			int numObjects = curChild.objects.Count;
// 			for (int j = numObjects - 1; j >= 0; j--) {
// 				OctreeObject curObj = curChild.objects[j];
// 				objects.Add(curObj);
// 			}
// 		}
// 		// Remove the child nodes (and the objects in them - they've been added elsewhere now)
// 		children = null;
// 	}
//
// 	/// <summary>
// 	/// Checks if outerBounds encapsulates innerBounds.
// 	/// </summary>
// 	/// <param name="outerBounds">Outer bounds.</param>
// 	/// <param name="innerBounds">Inner bounds.</param>
// 	/// <returns>True if innerBounds is fully encapsulated by outerBounds.</returns>
// 	static bool Encapsulates(Bounds outerBounds, Bounds innerBounds) {
// 		return outerBounds.Contains(innerBounds.min) && outerBounds.Contains(innerBounds.max);
// 	}
//
// 	/// <summary>
// 	/// Checks if there are few enough objects in this node and its children that the children should all be merged into this.
// 	/// </summary>
// 	/// <returns>True there are less or the same abount of objects in this and its children than numObjectsAllowed.</returns>
// 	bool ShouldMerge() {
// 		int totalObjects = objects.Count;
// 		if (children != null) {
// 			foreach (BoundsOctreeNode<T> child in children) {
// 				if (child.children != null) {
// 					// If any of the *children* have children, there are definitely too many to merge,
// 					// or the child woudl have been merged already
// 					return false;
// 				}
// 				totalObjects += child.objects.Count;
// 			}
// 		}
// 		return totalObjects <= NUM_OBJECTS_ALLOWED;
// 	}
// }
//
// public class BoundsOctree<T> {
// 	// The total amount of objects currently in the tree
// 	public int Count { get; private set; }
//
// 	// Root node of the octree
// 	BoundsOctreeNode<T> rootNode;
//
// 	// Should be a value between 1 and 2. A multiplier for the base size of a node.
// 	// 1.0 is a "normal" octree, while values > 1 have overlap
// 	readonly long looseness;
//
// 	// Size that the octree was on creation
// 	readonly long initialSize;
//
// 	// Minimum side length that a node can be - essentially an alternative to having a max depth
// 	readonly long minSize;
// 	// For collision visualisation. Automatically removed in builds.
// 	#if UNITY_EDITOR
// 	const int numCollisionsToSave = 4;
// 	readonly Queue<Bounds> lastBoundsCollisionChecks = new Queue<Bounds>();
// 	readonly Queue<Ray> lastRayCollisionChecks = new Queue<Ray>();
// 	#endif
//
// 	/// <summary>
// 	/// Constructor for the bounds octree.
// 	/// </summary>
// 	/// <param name="initialWorldSize">Size of the sides of the initial node, in metres. The octree will never shrink smaller than this.</param>
// 	/// <param name="initialWorldPos">Position of the centre of the initial node.</param>
// 	/// <param name="minNodeSize">Nodes will stop splitting if the new nodes would be smaller than this (metres).</param>
// 	/// <param name="loosenessVal">Clamped between 1 and 2. Values > 1 let nodes overlap.</param>
// 	public BoundsOctree(long initialWorldSize, Vector3 initialWorldPos, long minNodeSize, long loosenessVal) {
// 		if (minNodeSize > initialWorldSize) {
// 			Debug.LogWarning("Minimum node size must be at least as big as the initial world size. Was: " + minNodeSize + " Adjusted to: " + initialWorldSize);
// 			minNodeSize = initialWorldSize;
// 		}
// 		Count = 0;
// 		initialSize = initialWorldSize;
// 		minSize = minNodeSize;
// 		looseness = Mathf.Clamp(loosenessVal, 1.0f, 2.0f);
// 		rootNode = new BoundsOctreeNode<T>(initialSize, minSize, looseness, initialWorldPos);
// 	}
//
// 	// #### PUBLIC METHODS ####
//
// 	/// <summary>
// 	/// Add an object.
// 	/// </summary>
// 	/// <param name="obj">Object to add.</param>
// 	/// <param name="objBounds">3D bounding box around the object.</param>
// 	public void Add(T obj, Bounds objBounds) {
// 		// Add object or expand the octree until it can be added
// 		int count = 0; // Safety check against infinite/excessive growth
// 		while (!rootNode.Add(obj, objBounds)) {
// 			Grow(objBounds.center - rootNode.Center);
// 			if (++count > 20) {
// 				Debug.LogError("Aborted Add operation as it seemed to be going on forever (" + (count - 1) + ") attempts at growing the octree.");
// 				return;
// 			}
// 		}
// 		Count++;
// 	}
//
// 	/// <summary>
// 	/// Remove an object. Makes the assumption that the object only exists once in the tree.
// 	/// </summary>
// 	/// <param name="obj">Object to remove.</param>
// 	/// <returns>True if the object was removed successfully.</returns>
// 	public bool Remove(T obj) {
// 		bool removed = rootNode.Remove(obj);
//
// 		// See if we can shrink the octree down now that we've removed the item
// 		if (removed) {
// 			Count--;
// 			Shrink();
// 		}
//
// 		return removed;
// 	}
//
// 	/// <summary>
// 	/// Removes the specified object at the given position. Makes the assumption that the object only exists once in the tree.
// 	/// </summary>
// 	/// <param name="obj">Object to remove.</param>
// 	/// <param name="objBounds">3D bounding box around the object.</param>
// 	/// <returns>True if the object was removed successfully.</returns>
// 	public bool Remove(T obj, Bounds objBounds) {
// 		bool removed = rootNode.Remove(obj, objBounds);
//
// 		// See if we can shrink the octree down now that we've removed the item
// 		if (removed) {
// 			Count--;
// 			Shrink();
// 		}
//
// 		return removed;
// 	}
//
// 	/// <summary>
// 	/// Check if the specified bounds intersect with anything in the tree. See also: GetColliding.
// 	/// </summary>
// 	/// <param name="checkBounds">bounds to check.</param>
// 	/// <returns>True if there was a collision.</returns>
// 	public bool IsColliding(Bounds checkBounds) {
// 		//#if UNITY_EDITOR
// 		// For debugging
// 		//AddCollisionCheck(checkBounds);
// 		//#endif
// 		return rootNode.IsColliding(ref checkBounds);
// 	}
//
// 	/// <summary>
// 	/// Check if the specified ray intersects with anything in the tree. See also: GetColliding.
// 	/// </summary>
// 	/// <param name="checkRay">ray to check.</param>
// 	/// <param name="maxDistance">distance to check.</param>
// 	/// <returns>True if there was a collision.</returns>
// 	public bool IsColliding(Ray checkRay, long maxDistance) {
// 		//#if UNITY_EDITOR
// 		// For debugging
// 		//AddCollisionCheck(checkRay);
// 		//#endif
// 		return rootNode.IsColliding(ref checkRay, maxDistance);
// 	}
//
// 	/// <summary>
// 	/// Returns an array of objects that intersect with the specified bounds, if any. Otherwise returns an empty array. See also: IsColliding.
// 	/// </summary>
// 	/// <param name="collidingWith">list to store intersections.</param>
// 	/// <param name="checkBounds">bounds to check.</param>
// 	/// <returns>Objects that intersect with the specified bounds.</returns>
// 	public void GetColliding(List<T> collidingWith, Bounds checkBounds) {
// 		//#if UNITY_EDITOR
// 		// For debugging
// 		//AddCollisionCheck(checkBounds);
// 		//#endif
// 		rootNode.GetColliding(ref checkBounds, collidingWith);
// 	}
//
// 	/// <summary>
// 	/// Returns an array of objects that intersect with the specified ray, if any. Otherwise returns an empty array. See also: IsColliding.
// 	/// </summary>
// 	/// <param name="collidingWith">list to store intersections.</param>
// 	/// <param name="checkRay">ray to check.</param>
// 	/// <param name="maxDistance">distance to check.</param>
// 	/// <returns>Objects that intersect with the specified ray.</returns>
// 	public void GetColliding(List<T> collidingWith, Ray checkRay, long maxDistance = float.PositiveInfinity) {
// 		//#if UNITY_EDITOR
// 		// For debugging
// 		//AddCollisionCheck(checkRay);
// 		//#endif
// 		rootNode.GetColliding(ref checkRay, collidingWith, maxDistance);
// 	}
//
// 	public List<T> GetWithinFrustum(Camera cam) {
// 		var planes = GeometryUtility.CalculateFrustumPlanes(cam);
//
// 		var list = new List<T>();
// 		rootNode.GetWithinFrustum(planes, list);
// 		return list;
// 	}
//
// 	public Bounds GetMaxBounds() {
// 		return rootNode.GetBounds();
// 	}
//
// 	/// <summary>
// 	/// Draws node boundaries visually for debugging.
// 	/// Must be called from OnDrawGizmos externally. See also: DrawAllObjects.
// 	/// </summary>
// 	public void DrawAllBounds() {
// 		rootNode.DrawAllBounds();
// 	}
//
// 	/// <summary>
// 	/// Draws the bounds of all objects in the tree visually for debugging.
// 	/// Must be called from OnDrawGizmos externally. See also: DrawAllBounds.
// 	/// </summary>
// 	public void DrawAllObjects() {
// 		rootNode.DrawAllObjects();
// 	}
//
// 	/// <summary>
// 	/// Grow the octree to fit in all objects.
// 	/// </summary>
// 	/// <param name="direction">Direction to grow.</param>
// 	void Grow(Vector3 direction) {
// 		int xDirection = direction.x >= 0 ? 1 : -1;
// 		int yDirection = direction.y >= 0 ? 1 : -1;
// 		int zDirection = direction.z >= 0 ? 1 : -1;
// 		BoundsOctreeNode<T> oldRoot = rootNode;
// 		long half = rootNode.BaseLength / 2;
// 		long newLength = rootNode.BaseLength * 2;
// 		Vector3 newCenter = rootNode.Center + new Vector3(xDirection * half, yDirection * half, zDirection * half);
//
// 		// Create a new, bigger octree root node
// 		rootNode = new BoundsOctreeNode<T>(newLength, minSize, looseness, newCenter);
//
// 		if (oldRoot.HasAnyObjects()) {
// 			// Create 7 new octree children to go with the old root as children of the new root
// 			int rootPos = rootNode.BestFitChild(oldRoot.Center);
// 			BoundsOctreeNode<T>[] children = new BoundsOctreeNode<T>[8];
// 			for (int i = 0; i < 8; i++) {
// 				if (i == rootPos) {
// 					children[i] = oldRoot;
// 				}
// 				else {
// 					xDirection = i % 2 == 0 ? -1 : 1;
// 					yDirection = i > 3 ? -1 : 1;
// 					zDirection = (i < 2 || (i > 3 && i < 6)) ? -1 : 1;
// 					children[i] = new BoundsOctreeNode<T>(oldRoot.BaseLength, minSize, looseness, newCenter + new Vector3(xDirection * half, yDirection * half, zDirection * half));
// 				}
// 			}
//
// 			// Attach the new children to the new root node
// 			rootNode.SetChildren(children);
// 		}
// 	}
//
// 	/// <summary>
// 	/// Shrink the octree if possible, else leave it the same.
// 	/// </summary>
// 	void Shrink() {
// 		rootNode = rootNode.ShrinkIfPossible(initialSize);
// 	}
// }
}