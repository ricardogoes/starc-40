import { OnInit, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { TreeService } from './tree.service';
import * as TreeTypes from './tree.types';
import { Tree } from './tree';
export declare class TreeComponent implements OnInit, OnChanges {
    private treeService;
    private static EMPTY_TREE;
    treeModel: TreeTypes.TreeModel;
    settings: TreeTypes.Ng2TreeSettings;
    nodeCreated: EventEmitter<any>;
    nodeRemoved: EventEmitter<any>;
    nodeRenamed: EventEmitter<any>;
    nodeSelected: EventEmitter<any>;
    nodeMoved: EventEmitter<any>;
    nodeExpanded: EventEmitter<any>;
    nodeCollapsed: EventEmitter<any>;
    tree: Tree;
    constructor(treeService: TreeService);
    ngOnChanges(changes: SimpleChanges): void;
    ngOnInit(): void;
}
