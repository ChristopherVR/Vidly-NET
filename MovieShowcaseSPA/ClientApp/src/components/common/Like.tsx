import React from 'react';

// TODO: Add font awesome icons.

function Like({ liked, onClick }: { liked: boolean; onClick: () => void }) {
  let classes = 'fa fa-heart';
  if (!liked) classes += '-o';
  return (
    <i
      onClick={onClick}
      style={{ cursor: 'pointer' }}
      className={classes}
      aria-hidden="true"
    />
  );
}

export default Like;
