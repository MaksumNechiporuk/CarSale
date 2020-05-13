import React, { Fragment } from "react";
import { NavLink } from 'react-router-dom';

const AdminPanel = () => {

    

    return (
        <Fragment>
            <div class="admin-panel">
                <div class="slidebar">
                    <ul>
                        <li><NavLink activeClassName="active" exact href="#" to="/AdminPanel/inprogress" className="nav-item"><i class="fa fa-tachometer"></i>General</NavLink></li>
                        <li><NavLink  exact href="#" to="/AdminPanel/inprogress" className="nav-item"><i class="fa fa-users"></i>Users</NavLink></li>
                        <li><NavLink  exact href="#" to="/AdminPanel/filters" className="nav-item"><i class="fa fa-filter"></i>Filters</NavLink></li>
                        <li><NavLink  exact href="#" to="/AdminPanel/inprogress" className="nav-item"><i class="fa fa-car"></i>Cars</NavLink></li>
                        <li><NavLink  exact href="#" to="/AdminPanel/inprogress" className="nav-item"><i class="fa fa-file-video-o"></i>In Progress</NavLink></li>
                        <li><NavLink  exact href="#" to="/AdminPanel/inprogress" className="nav-item"><i class="fa fa-wrench"></i>In Progress</NavLink></li>
                    </ul>
                </div>
            </div>
        </Fragment>
    );
}

export default AdminPanel;
