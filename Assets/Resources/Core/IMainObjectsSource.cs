using UnityEngine;

namespace Resources.Core {
    public interface IMainObjectsSource {
        GameObject GetPlayer();
        Area GetVisibleArea();
    }
}