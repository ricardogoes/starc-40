export declare enum NodeMenuItemAction {
    NewTestCase = 0,
    NewTestSuite = 1,
    Rename = 2,
    Remove = 3,
}
export declare enum NodeMenuAction {
    Close = 0,
}
export interface NodeMenuEvent {
    sender: HTMLElement;
    action: NodeMenuAction;
}
export interface NodeMenuItemSelectedEvent {
    nodeMenuItemAction: NodeMenuItemAction;
}
