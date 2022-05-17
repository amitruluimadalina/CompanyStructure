import React from "react";
import Navigation from '../components/Navigation';
import styles from './HomeViewStyles.module.scss'
const HomeView = () => {
    const navLinks=[{linkName: 'Department', linkPath: '/department'},{linkName: 'Employee', linkPath: '/employee'}];
    return (
    <div className={styles.geometric}>
        <Navigation links={navLinks} variant="dark"/>
    </div>
       
    );
};
export default HomeView;