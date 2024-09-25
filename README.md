
<p align="center">
  <a href="https://openupm.com/packages/cn.punctual-solutions.tool/">
    <img src="https://img.shields.io/npm/v/cn.punctual-solutions.tool?label=openupm&amp;registry_uri=https://package.openupm.com" />
  </a>
</p>

# Punctual Solutions Tool
这是一个简化unity开发的工具包
## 功能
### 单例
优点：不占用父类，使用SourceGenerator在每个类型里单独生成

注意事项：SingletonAttribute标记Mono类无效，SingletonMonoAttribute标记普通类无效
#### 普通类单例
```csharp
[Singleton]
public partial class Singleton
{
    public void OnSingletonInit()
    {
        
    }
    
    public void Dispose()
    {
    
    }
}
```
##### SingletonAttribute
作用：将普通类标记为单例类

参数：
* NotAllowedDispose： 不允许调用Dispose方法，否则会抛出异常
* DisableLazyLoad：不使用Lazy加载方式，会导致多线程下不安全
* DisableSealed：禁用sealed标记，默认使用sealed标记
#### ISingleton
作用：所有mono和普通类标记为Singleton后会自动实现的接口

OnSingletonInit：初始化时会调用

Dispose：释放时会调用，可以手动调用进行释放
#### mono 类单例
```csharp
[SingletonMono]
public partial class Singleton : MonoBehaviour
{
    public void OnSingletonInit()
    {
        
    }
    
    public void Dispose()
    {
    
    }
}
```
如果用各种方式在实例已经存在的情况下再次添加，则会抛出异常

会占用Awake方法，需要调用Awake方法请用InAwake替代

InDispose:自动生成的方法，会销毁当前单例组件

自动生成的Singleton会被附加到名为Singleton的游戏物体，AllowAutoDestroy为true则会挂载到SingletonAutoDestroy
##### SingletonMonoAttribute
作用：将Mono类标记为单例类

参数：
* 大部分与SingletonAttribute一致
* AllowAutoDestroy：启用后切换场景后自动销毁
## 没实现的功能
### 不准备实现
线程锁：已使用了更好的Lazy加载方式

MonoSingleton 在子游戏物体下生成：没想到应用场景，还可能造成管理混乱

MonoSingleton 再次创建自动移除新的或者移除旧的：没有想到应用场景，还可能造成管理混乱

MonoSingleton 必须加载到单独GameObject：没有想到应用场景，需要单独加载可以自行创建游戏物体并添加组件，没必要用这种方式

MonoSingleton 修改自动加载挂载到的默认GameObject名称：没有想到应用场景，可以自行创建游戏物体并添加组件，没必要用这种方式

饿汉式加载方式：占用内存，并且没遇到过这种使用场景
### 在计划中
SingletonAttribute标记Mono类无效，SingletonMono标记普通类无效,如果错误标记则会提示错误无法通过编译，现在只会不生成代码而不会提示
### 考虑实现
CAS单例：性能一般不会和Lazy差距太大，有具体使用场景后会实现

能够自动进行延迟释放，比如延迟30s没有访问自动释放：有具体使用场景后会实现
### 考虑废弃
