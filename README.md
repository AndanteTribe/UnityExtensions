# UnityExtensions
[![unity-meta-check](https://github.com/AndanteTribe/UnityExtensions/actions/workflows/unity-meta-check.yml/badge.svg)](https://github.com/AndanteTribe/UnityExtensions/actions/workflows/unity-meta-check.yml)
[![Releases](https://img.shields.io/github/release/AndanteTribe/UnityExtensions.svg)](https://github.com/AndanteTribe/UnityExtensions/releases)
[![GitHub license](https://img.shields.io/github/license/AndanteTribe/UnityExtensions.svg)](./LICENSE)

English | [日本語](README_JA.md)

## Overview
**UnityExtensions** is a library that provides extension methods for Unity's core components and types.

It offers safe, performant, and convenient extension methods for commonly used Unity classes such as `Component`, `GameObject`, `Transform`, `RectTransform`, and `Vector3`.

## Requirements
- Unity 2022.3 or later

## Installation
Open `Window > Package Manager`, select `[+] > Add package from git URL`, and enter the following URL:

```
https://github.com/AndanteTribe/UnityExtensions.git?path=src/UnityExtensions.Unity/Packages/jp.andantetribe.unityextensions
```

## API

### ComponentExtensions

#### `SafeGetComponent<T>()`
A safe alternative to `Component.GetComponent<T>()`. Returns System's `null` instead of throwing if the component is not found.

```csharp
using UnityExtensions;
using UnityEngine;

public class Example : MonoBehaviour
{
    private HingeJoint _hinge;

    private void Update()
    {
        _hinge ??= this.SafeGetComponent<HingeJoint>();
        if (_hinge != null) _hinge.useSpring = false;
    }
}
```

### GameObjectExtensions

#### `SafeGetComponent<T>()`
A safe alternative to `GameObject.GetComponent<T>()`. Returns System's `null` instead of throwing if the component is not found.

```csharp
using UnityExtensions;
using UnityEngine;

public class Example : MonoBehaviour
{
    private HingeJoint _hinge;

    private void Update()
    {
        _hinge ??= this.gameObject.SafeGetComponent<HingeJoint>();
        if (_hinge != null) _hinge.useSpring = false;
    }
}
```

#### `GetHierarchyPath(bool includeScene = true)`
Gets the full hierarchy path of a `GameObject`.

```csharp
using UnityExtensions;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void Start()
    {
        // Include scene name (default)
        Debug.Log(this.gameObject.GetHierarchyPath());
        // Exclude scene name
        Debug.Log(this.gameObject.GetHierarchyPath(false));
    }
}
```

### TransformExtensions

Extension methods for setting or modifying individual components of `Transform.position`.

| Method | Description |
|--------|-------------|
| `SetPosX(float x)` | Sets the X component of `position`. |
| `AddPosX(float x)` | Adds to the X component of `position`. |
| `SubPosX(float x)` | Subtracts from the X component of `position`. |
| `SetPosY(float y)` | Sets the Y component of `position`. |
| `AddPosY(float y)` | Adds to the Y component of `position`. |
| `SubPosY(float y)` | Subtracts from the Y component of `position`. |
| `SetPosZ(float z)` | Sets the Z component of `position`. |
| `AddPosZ(float z)` | Adds to the Z component of `position`. |
| `SubPosZ(float z)` | Subtracts from the Z component of `position`. |

```csharp
using UnityExtensions;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void Update()
    {
        transform.SetPosX(1.0f);
        transform.AddPosY(0.1f);
        transform.SubPosZ(0.5f);
    }
}
```

### RectTransformExtensions

Extension methods for `RectTransform`, offering safer alternatives to `sizeDelta`.

#### `SetSize(float width, float height)`
Sets the width and height of the `RectTransform` using `SetSizeWithCurrentAnchors`.

```csharp
using UnityExtensions;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void Start()
    {
        RectTransform rectTransform = (RectTransform)transform;
        rectTransform.SetSize(100f, 100f);
    }
}
```

#### `SetSize(Vector2 size)`
Sets the size of the `RectTransform` using a `Vector2`.

```csharp
using UnityExtensions;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void Start()
    {
        RectTransform rectTransform = (RectTransform)transform;
        var size = new Vector2(100f, 100f);
        rectTransform.SetSize(size);
    }
}
```

#### `SetWidth(float width)`
Sets only the width of the `RectTransform`.

```csharp
using UnityExtensions;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void Start()
    {
        RectTransform rectTransform = (RectTransform)transform;
        rectTransform.SetWidth(100f);
    }
}
```

#### `SetHeight(float height)`
Sets only the height of the `RectTransform`.

```csharp
using UnityExtensions;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void Start()
    {
        RectTransform rectTransform = (RectTransform)transform;
        rectTransform.SetHeight(100f);
    }
}
```

#### `GetSize()`
Gets the actual rendered size of the `RectTransform` via `rect.size`, which is more reliable than `sizeDelta`.

```csharp
using UnityExtensions;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void Start()
    {
        RectTransform rectTransform = (RectTransform)transform;
        Vector2 size = rectTransform.GetSize();
        Debug.Log("Size : " + size.ToString());
    }
}
```

#### `SetFullStretch(float left = 0, float right = 0, float top = 0, float bottom = 0)`
Configures the `RectTransform` to fully stretch (anchor min `(0,0)`, anchor max `(1,1)`) with optional edge offsets.

```csharp
using UnityExtensions;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void Start()
    {
        RectTransform rectTransform = (RectTransform)transform;

        // Full stretch with no offsets
        rectTransform.SetFullStretch();

        // Full stretch with 10px padding on each side
        rectTransform.SetFullStretch(left: 10f, right: 10f, top: 10f, bottom: 10f);
    }
}
```

#### `GetWorldCorners(Span<Vector3> fourCornersSpan)`
Gets the four corners of the rectangle in world space. The `Span<Vector3>` must have a length of at least 4.  
Corner order: bottom-left, top-left, top-right, bottom-right.

```csharp
using System;
using UnityExtensions;
using UnityEngine;

public class Example : MonoBehaviour
{
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = (RectTransform)transform;

        var corners = (stackalloc Vector3[4]);
        _rectTransform.GetWorldCorners(corners);

        Debug.Log("World Corners");
        for (var i = 0; i < 4; i++)
        {
            Debug.Log("World Corner " + i + " : " + corners[i]);
        }
    }
}
```

#### `GetLocalCorners(Span<Vector3> fourCornersSpan)`
Gets the four corners of the rectangle in the local space of the `RectTransform`. The `Span<Vector3>` must have a length of at least 4.  
Corner order: bottom-left, top-left, top-right, bottom-right.

```csharp
using System;
using UnityExtensions;
using UnityEngine;

public class Example : MonoBehaviour
{
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = (RectTransform)transform;

        var corners = (stackalloc Vector3[4]);
        _rectTransform.rotation = Quaternion.AngleAxis(45, Vector3.forward);
        _rectTransform.GetLocalCorners(corners);

        Debug.Log("Local Corners");
        for (var i = 0; i < 4; i++)
        {
            Debug.Log("Local Corner " + i + " : " + corners[i]);
        }
    }
}
```

### VectorExtensions

Extension methods for `Vector3`.

| Method | Description |
|--------|-------------|
| `XZ()` | Extracts the X and Z components as a `Vector2`. |
| `YZ()` | Extracts the Y and Z components as a `Vector2`. |

```csharp
using UnityExtensions;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void Start()
    {
        Vector3 v = new Vector3(1f, 2f, 3f);
        Vector2 xz = v.XZ(); // (1, 3)
        Vector2 yz = v.YZ(); // (2, 3)
    }
}
```

## License
This library is released under the MIT license.
