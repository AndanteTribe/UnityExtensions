# UnityExtensions
[![unity-meta-check](https://github.com/AndanteTribe/UnityExtensions/actions/workflows/unity-meta-check.yml/badge.svg)](https://github.com/AndanteTribe/UnityExtensions/actions/workflows/unity-meta-check.yml)
[![Releases](https://img.shields.io/github/release/AndanteTribe/UnityExtensions.svg)](https://github.com/AndanteTribe/UnityExtensions/releases)
[![GitHub license](https://img.shields.io/github/license/AndanteTribe/UnityExtensions.svg)](./LICENSE)
[![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/AndanteTribe/UnityExtensions)

English | [日本語](README_JA.md)

## Overview
**UnityExtensions** is a library that provides extension methods for Unity's core components and types.

It offers safe, performant, and convenient extension methods for commonly used Unity classes such as `Component`, `GameObject`, `Transform`, `RectTransform`, and `Vector3`.

## Requirements
- Unity 2021.3 or later

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
| `SetX(float x)` | Sets the X component of `position`. |
| `AddX(float x)` | Adds to the X component of `position`. |
| `SubX(float x)` | Subtracts from the X component of `position`. |
| `SetY(float y)` | Sets the Y component of `position`. |
| `AddY(float y)` | Adds to the Y component of `position`. |
| `SubY(float y)` | Subtracts from the Y component of `position`. |
| `SetZ(float z)` | Sets the Z component of `position`. |
| `AddZ(float z)` | Adds to the Z component of `position`. |
| `SubZ(float z)` | Subtracts from the Z component of `position`. |

```csharp
using UnityExtensions;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void Update()
    {
        transform.SetX(1.0f);
        transform.AddY(0.1f);
        transform.SubZ(0.5f);
    }
}
```

### RectTransformExtensions

Extension methods for `RectTransform`, offering safer alternatives to `sizeDelta`.

| Method | Description |
|--------|-------------|
| `SetSize(float width, float height)` | Sets the size of the `RectTransform`. |
| `SetSize(Vector2 size)` | Sets the size of the `RectTransform` using a `Vector2`. |
| `SetWidth(float width)` | Sets the width of the `RectTransform`. |
| `SetHeight(float height)` | Sets the height of the `RectTransform`. |
| `GetSize()` | Gets the size of the `RectTransform`. |
| `SetFullStretch(float left, float right, float top, float bottom)` | Stretches the `RectTransform` fully (stretch × stretch). |
| `GetWorldCorners(Span<Vector3>)` | Gets the four corners of the rectangle in world space. |
| `GetLocalCorners(Span<Vector3>)` | Gets the four corners of the rectangle in local space. |

```csharp
using UnityExtensions;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void Start()
    {
        RectTransform rectTransform = (RectTransform)transform;

        rectTransform.SetSize(100f, 200f);
        rectTransform.SetFullStretch();

        Vector2 size = rectTransform.GetSize();
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
