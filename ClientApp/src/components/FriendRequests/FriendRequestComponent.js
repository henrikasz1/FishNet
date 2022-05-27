import React, { useEffect, useState } from 'react';
import { View, StyleSheet } from 'react-native';

const styles = StyleSheet.create({
    requestBox: {
        width: '33%'
    }
})

export default function FriendRequestsComponent({ userId }){

    const [friends, setFriends] = useState([]);

    const getPendingFriendRequests = () => {
        //pasiduodi user id
    }

    const acceptFriendRequest = (friendId) => {

    }

    const rejectFriendRequest = (friendId) => {
        
    }

    useEffect(() => {
        getPendingFriendRequests().then(_friends => setFriends(_friends));
    }, []);

    return (
        <View>
            {friends.map(friendRequest => (
                <View style={styles.requestBox}>
                    {/** profile picture, \n name, \n accept, reject, kiti duomenys: id... */}
                </View>
            ))}
        </View>
    )
}