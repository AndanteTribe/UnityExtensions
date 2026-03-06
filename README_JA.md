# UnityExtensions
[![unity-meta-check](https://github.com/AndanteTribe/UnityExtensions/actions/workflows/unity-meta-check.yml/badge.svg)](https://github.com/AndanteTribe/UnityExtensions/actions/workflows/unity-meta-check.yml)
[![Releases](https://img.shields.io/github/release/AndanteTribe/UnityExtensions.svg)](https://github.com/AndanteTribe/UnityExtensions/releases)
[![GitHub license](https://img.shields.io/github/license/AndanteTribe/UnityExtensions.svg)](./LICENSE)

[English](README.md) | 日本語

## 概要
**UnityExtensions** は、Unity のコアコンポーネントおよび型に対する拡張メソッドを提供するライブラリです。

`Component`、`GameObject`、`Transform`、`RectTransform`、`Vector3` など、Unity でよく使用されるクラスに対して、安全で高パフォーマンスかつ便利な拡張メソッドを提供します。

## 動作要件
- Unity 2022.3 以降

## インストール
`Window > Package Manager` を開き、`[+] > Add package from git URL` を選択し、以下の URL を入力してください。

```
https://github.com/AndanteTribe/UnityExtensions.git?path=src/UnityExtensions.Unity/Packages/jp.andantetribe.unityextensions
```

## API

### ComponentExtensions

#### `SafeGetComponent<T>()`
`Component.GetComponent<T>()` の安全な代替メソッドです。コンポーネントが見つからない場合、例外をスローする代わりに System の `null` を返します。

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
`GameObject.GetComponent<T>()` の安全な代替メソッドです。コンポーネントが見つからない場合、例外をスローする代わりに System の `null` を返します。

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
`GameObject` のヒエラルキーパスを取得します。

```csharp
using UnityExtensions;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void Start()
    {
        // シーン名を含む（デフォルト）
        Debug.Log(this.gameObject.GetHierarchyPath());
        // シーン名を含まない
        Debug.Log(this.gameObject.GetHierarchyPath(false));
    }
}
```

### TransformExtensions

`Transform.position` の各成分を設定・変更するための拡張メソッドです。

| メソッド | 説明 |
|--------|------|
| `SetX(float x)` | `position` の X 成分を設定します。 |
| `AddX(float x)` | `position` の X 成分に加算します。 |
| `SubX(float x)` | `position` の X 成分から減算します。 |
| `SetY(float y)` | `position` の Y 成分を設定します。 |
| `AddY(float y)` | `position` の Y 成分に加算します。 |
| `SubY(float y)` | `position` の Y 成分から減算します。 |
| `SetZ(float z)` | `position` の Z 成分を設定します。 |
| `AddZ(float z)` | `position` の Z 成分に加算します。 |
| `SubZ(float z)` | `position` の Z 成分から減算します。 |

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

`sizeDelta` を使用するよりも安全な `RectTransform` の拡張メソッドです。

#### `SetSize(float width, float height)`
`SetSizeWithCurrentAnchors` を使用して `RectTransform` の幅と高さを設定します。

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
`Vector2` を使用して `RectTransform` のサイズを設定します。

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
`RectTransform` の幅のみを設定します。

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
`RectTransform` の高さのみを設定します。

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
`rect.size` を通じて `RectTransform` の実際の描画サイズを取得します。`sizeDelta` よりも信頼性が高い方法です。

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
`RectTransform` を完全にストレッチするよう設定します（アンカー min `(0,0)`、アンカー max `(1,1)`）。各辺のオフセットをオプションで指定できます。

```csharp
using UnityExtensions;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void Start()
    {
        RectTransform rectTransform = (RectTransform)transform;

        // オフセットなしで完全にストレッチ
        rectTransform.SetFullStretch();

        // 各辺に 10px のパディングを設定してストレッチ
        rectTransform.SetFullStretch(left: 10f, right: 10f, top: 10f, bottom: 10f);
    }
}
```

#### `GetWorldCorners(Span<Vector3> fourCornersSpan)`
ワールド空間における矩形の 4 隅を取得します。`Span<Vector3>` の長さは 4 以上である必要があります。  
隅の順序: 左下、左上、右上、右下。

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
`RectTransform` のローカル空間における矩形の 4 隅を取得します。`Span<Vector3>` の長さは 4 以上である必要があります。  
隅の順序: 左下、左上、右上、右下。

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

`Vector3` に対する拡張メソッドです。

| メソッド | 説明 |
|--------|------|
| `XZ()` | X 成分と Z 成分を `Vector2` として取得します。 |
| `YZ()` | Y 成分と Z 成分を `Vector2` として取得します。 |

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

## ライセンス
このライブラリは MIT ライセンスのもとで公開されています。
