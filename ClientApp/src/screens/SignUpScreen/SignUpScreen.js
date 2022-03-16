import { View, Text, StyleSheet, useWindowDimensions, ScrollView} from 'react-native';
import React, {useState} from 'react';
import { Switch } from 'react-native-paper';
import CustomInput from '../../components/CustomInput';
import CustomButton from '../../components/CustomButton';

const SignUpScreen = () => {
  const [firstName, setFirstName] = useState('')
  const [lastName, setLastName] = useState('')
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [passwordRepeat, setPasswordRepeat] = useState('')
  const [isProfilePrivate, setIsProfilePrivate] = useState(false)

  const {height} = useWindowDimensions();

  const onRegisterPressed = () => {
      console.warn("good boy!")
  }

  return (
    <ScrollView showsVerticalScrollIndicator={false}>
        <View style={styles.root}>

            

            <Text style={styles.title}> Create new account </Text>

            <CustomInput
                placeholder="First Name"
                iconType="user"
                value={firstName}
                setValue={setFirstName}
            />

            <CustomInput
                placeholder="Last Name"
                iconType="user"
                value={lastName}
                setValue={setLastName}
            />
            
            <CustomInput
                placeholder="Email address"
                iconType="envelope"
                value={email}
                setValue={setEmail}
            />

            <CustomInput
                placeholder="Password"
                iconType="lock"
                value={password}
                setValue={setPassword}
                secureTextEntry={true}
            />

            
            <CustomInput
                placeholder="Repeat Password"
                iconType="lock-open"
                value={passwordRepeat}
                setValue={setPasswordRepeat}
                secureTextEntry={true}
            />

            <View style={styles.private}>
                <Switch
                    style={styles.toggle}
                    value={isProfilePrivate}
                    onValueChange={setIsProfilePrivate}
                />
                <Text> Make your profile private </Text>
            </View>

            <CustomButton 
                text="Register"
                onPress={onRegisterPressed}
                type="primary"
            />

            <Text style={styles.policy}>
                By registering, you confirm that you accept our 
                <Text style={styles.link}> Terms of Use </Text> and
                <Text style={styles.link}> Privacy Policy </Text>
            </Text>

        </View>
    </ScrollView>
  )
}

const styles = StyleSheet.create({
    root: {
        alignItems: 'center',
        padding: 20,
    },
    logo: {
        width: '70%',
        maxWidth: 200,
        maxHeight: 200,
    },
    title: {
        fontSize: 24,
        fontWeight: 'bold',
        color: '#051C60',
        margin: 25,
    },
    toggle: {
        transform: [{ scaleX: 1.6}, {scaleY: 1.6}],
        marginRight: "5%",
        marginBottom: "10%"
    },
    private: {
        marginTop: '7%',
        width: '95%',
        flex: 1,
        flexDirection: 'row'
    },
    policy: {
        color: 'grey',
        padding: '2%'
    },
    link: {
        color: '#FD8075'
    }
});

export default SignUpScreen