"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var fn_utils_1 = require("./utils/fn.utils");
var FoldingType = (function () {
    function FoldingType(_cssClass) {
        this._cssClass = _cssClass;
    }
    Object.defineProperty(FoldingType.prototype, "cssClass", {
        get: function () {
            return this._cssClass;
        },
        enumerable: true,
        configurable: true
    });
    return FoldingType;
}());
FoldingType.Expanded = new FoldingType('node-expanded');
FoldingType.Collapsed = new FoldingType('node-collapsed');
FoldingType.Empty = new FoldingType('node-empty');
FoldingType.Leaf = new FoldingType('node-leaf');
exports.FoldingType = FoldingType;
var TreeModelSettings = (function () {
    function TreeModelSettings() {
    }
    TreeModelSettings.merge = function (sourceA, sourceB) {
        return fn_utils_1.defaultsDeep({}, fn_utils_1.get(sourceA, 'settings'), fn_utils_1.get(sourceB, 'settings'), { static: false, leftMenu: false, rightMenu: true });
    };
    return TreeModelSettings;
}());
exports.TreeModelSettings = TreeModelSettings;
var TreeStatus;
(function (TreeStatus) {
    TreeStatus[TreeStatus["New"] = 0] = "New";
    TreeStatus[TreeStatus["Modified"] = 1] = "Modified";
    TreeStatus[TreeStatus["IsBeingRenamed"] = 2] = "IsBeingRenamed";
})(TreeStatus = exports.TreeStatus || (exports.TreeStatus = {}));
//# sourceMappingURL=tree.types.js.map