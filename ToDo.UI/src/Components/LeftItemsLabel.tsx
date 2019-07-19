import React from 'react';
import { LeftItemsLabelComponentProps } from '../Models';

export const LeftItemsLabel: React.FC<LeftItemsLabelComponentProps> = (props) => {
  return (
    <div style={props.style}>{props.noItems} {props.noItems == 1 ? 'item' : 'items'} left</div>
  )
}
