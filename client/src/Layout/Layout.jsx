import React from 'react';
import Style from './Layout.scss';

const Layout = props => {
    const { children } = props;

    return (
        <div>
            <header className={Style.Header}>
                <h1>Indexed Files</h1>
            </header>
            <main>
                {children}
            </main>
        </div>
    );
};

export default Layout;
