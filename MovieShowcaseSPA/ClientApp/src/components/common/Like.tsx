import { faHeart, faHeartBroken } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React from 'react';

type LikeProps = {
  liked: boolean;
  onClick: () => void;
};

function Like({ liked, onClick }: LikeProps) {
  return (
    <FontAwesomeIcon
      onClick={onClick}
      role="button"
      icon={liked ? faHeart : faHeartBroken}
      aria-hidden="true"
    />
  );
}

export default Like;
