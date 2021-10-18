import React, { Component } from 'react';
import axios from '../../axiosConfig';
import Input from './Input/Input';
import TextArea from './TextArea/TextArea';
import Button from './Button/Button';
import Style from './DataBaseHandler.scss';

class DataBaseHandler extends Component {
    constructor(props) {
        super(props);
        this.state = {
            objectArea: [],
            indexArea: [],
            data: '',
            id: '',
            disabled: true,
        };
    }

    onChange = (event, name) => {
        const { [name]: value } = this.state;
        if (value !== event.target.value) {
            this.setState({
                [name]: event.target.value,
            });
        }
    }

    handleAdd = (event) => {
        const { id, data } = this.state;

        if (!data)
        {
            alert("Data field can not be empty");
            return;
        }

        axios.
        post('indexedfiles', {
            data: data,
        }).
        then(
            response => {
                this.setState({
                    objectArea: response.data.objectArea,
                    indexArea: response.data.indexArea,
                });
                alert(
                    `Element successfully added to DataBase`,
                );
            }).
        catch(
            error => {
                alert(`Error with code ${error.status}`);
            });
    }

    handleRemove = (event) => {
        const { id } = this.state;

        if (!id)
        {
            alert("Id field can not be empty");
            return;
        }

        axios.
        delete(`indexedfiles/${id}`).
        then(
            response => {
                this.setState({
                    objectArea: response.data.objectArea,
                    indexArea: response.data.indexArea,
                });
                alert(
                    `Element successfully removed from DataBase`,
                );
            }).
        catch(
            error => {
                alert(`Error with code ${error.status}`);
            });
    }

    componentDidMount() {
        axios.get('indexedfiles').then(res => {
            this.setState({
                objectArea: res.data.objectArea,
                indexArea: res.data.indexArea,
            });
        });
    }

    render() {
        const { objectArea, indexArea, data, id, disabled } = this.state;
        return (
            <div className={Style.Wrapper}>
                    <Input
                        labelText="Enter a key data:"
                        placeholder="Hello world"
                        name="data"
                        type="text"
                        value={data}
                        onChange={event => this.onChange(event, 'data')}
                    />
                    <Input
                        labelText="Enter a key id:"
                        placeholder="1"
                        name="id"
                        type="text"
                        value={id}
                        onChange={event => this.onChange(event, 'id')}
                    />
                    <div className={Style.ButtonWrapper}>
                        <Button
                            name="Add element"
                            type="Add element"
                            onClick={this.handleAdd}>
                            Add element
                        </Button>
                        <Button
                            name="Remove element"
                            type="Remove element"
                            onClick={this.handleRemove}>
                            Remove element
                        </Button>
                    </div>
                    <div className={Style.TextAreaWrapper}>
                        <TextArea
                            labelText="Object area"
                            disabled={disabled}>
                            {objectArea.join("\n")}
                        </TextArea>
                        <TextArea
                            labelText="Index area"
                            disabled={disabled}>
                            {indexArea.join("\n")}
                        </TextArea>
                    </div>
            </div>
        );
    };
}

export default DataBaseHandler;
