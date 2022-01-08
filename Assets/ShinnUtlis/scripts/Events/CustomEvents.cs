using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BoolEvent : UnityEvent<bool> { }
[System.Serializable]
public class BoolArrayEvent : UnityEvent<bool[]> { }

[System.Serializable]
public class IntEvent : UnityEvent<int> { }
[System.Serializable]
public class IntArrayEvent : UnityEvent<int[]> { }

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }
[System.Serializable]
public class FloatArrayEvent : UnityEvent<float[]> { }

[System.Serializable]
public class StringEvent : UnityEvent<string> { }
[System.Serializable]
public class StringArratEvent : UnityEvent<string[]> { }

[System.Serializable]
public class Vector3Event : UnityEvent<Vector3> { }
[System.Serializable]
public class Vector3ArrrayEvent : UnityEvent<Vector3[]> { }

[System.Serializable]
public class ColorEvent : UnityEvent<Color> { }
[System.Serializable]
public class ColorArrayEvent : UnityEvent<Color[]> { }

[System.Serializable]
public class VoidEvent : UnityEvent { }
