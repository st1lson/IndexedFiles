import React from 'react';
import Style from './TextArea.scss';

const TextArea = props => {
    const { labelText, disabled, name, type, value, children } = props;
    return (
        <div className={Style.Wrapper}>
            <label>{labelText}</label>
            <textArea
                name={name}
                type={type}
                disabled={disabled}
                value={value}>
                {children}
            </textArea>
        </div>
    );
};

export default TextArea;
