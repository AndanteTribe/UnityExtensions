# UnityExtensions
[![unity-meta-check](https://github.com/AndanteTribe/UnityExtensions/actions/workflows/unity-meta-check.yml/badge.svg)](https://github.com/AndanteTribe/UnityExtensions/actions/workflows/unity-meta-check.yml)
[![Releases](https://img.shields.io/github/release/AndanteTribe/UnityExtensions.svg)](https://github.com/AndanteTribe/UnityExtensions/releases)
[![GitHub license](https://img.shields.io/github/license/AndanteTribe/UnityExtensions.svg)](./LICENSE)
[![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/AndanteTribe/UnityExtensions)

[English](README.md) | 日本語

## 概要
**UnityExtensions** は、Unity のコアコンポーネントおよび型に対する拡張メソッドを提供するライブラリです。

`Component`、`GameObject`、`Transform`、`RectTransform`、`Vector3` など、Unity でよく使用されるクラスに対して、安全で高パフォーマンスかつ便利な拡張メソッドを提供します。

## 動作要件
- Unity 2021.3 以降

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

| メソッド | 説明 |
|--------|------|
| `SetSize(float width, float height)` | `RectTransform` のサイズを設定します。 |
| `SetSize(Vector2 size)` | `Vector2` を使用して `RectTransform` のサイズを設定します。 |
| `SetWidth(float width)` | `RectTransform` の幅を設定します。 |
| `SetHeight(float height)` | `RectTransform` の高さを設定します。 |
| `GetSize()` | `RectTransform` のサイズを取得します。 |
| `SetFullStretch(float left, float right, float top, float bottom)` | `RectTransform` を完全に引き伸ばします（ストレッチ × ストレッチ）。 |
| `GetWorldCorners(Span<Vector3>)` | ワールド空間における矩形の 4 隅を取得します。 |
| `GetLocalCorners(Span<Vector3>)` | ローカル空間における矩形の 4 隅を取得します。 |

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
