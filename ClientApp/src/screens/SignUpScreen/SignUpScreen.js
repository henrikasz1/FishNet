import { View, Text, StyleSheet, useWindowDimensions, ScrollView, ActivityIndicator} from 'react-native';
import React, {useState} from 'react';
import { Switch } from 'react-native-paper';
import { useNavigation } from '@react-navigation/native';
import CustomInput from '../../components/CustomInput';
import CustomButton from '../../components/CustomButton';
import CustomMsgBox from '../../components/CustomMessageBox';
import Icon from 'react-native-vector-icons/dist/SimpleLineIcons';
import axios from 'axios';

const SignUpScreen = () => {
  const [firstName, setFirstName] = useState('')
  const [lastName, setLastName] = useState('')
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [passwordRepeat, setPasswordRepeat] = useState('')
  const [isProfilePrivate, setIsProfilePrivate] = useState(false)
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [message, setMessage] = useState('')

  const {height} = useWindowDimensions();
  const navigation = useNavigation();

  const onRegisterPressed = () => {
      setIsSubmitting(true)
      handleMessage('')
      if (email == '' || password == '' || firstName == '', lastName == '', passwordRepeat == '') {
          handleMessage('Please fill in all fields');
          setIsSubmitting(false);
      }
      else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(email))
      {
         handleMessage('Invalid email address')
         setIsSubmitting(false)
      }
      else if (password !== passwordRepeat)
      {
          handleMessage('Passwords do not match')
          setIsSubmitting(false)
      }
      else {
          handleRegister({firstName, lastName, email, password, isProfilePrivate});
      }
  }
  const onGoBackPressed = () => {
      navigation.goBack();
  }

  const handleRegister = (credentials) => {
    const url = "http://10.0.2.2:5000/api/user/register";

    axios
        .post(url, credentials)
        .then((response) => {
            const result = response.data;
            const {token, success, errors} = result;
            axios.defaults.headers.common['Authorization'] = 'Bearer ' + token;
            navigation.navigate('MainScreen');
            console.warn("success")
        })
        .catch(error => {
            setIsSubmitting(false)
            console.warn(error.response.data.errors)
            handleMessage(error.response.data.errors)
        })
  }

  const handleMessage = (message) => {
      setMessage(message);
  }

  return (
    <ScrollView showsVerticalScrollIndicator={false}>
        <View style={styles.root}>

            <View style={styles.header}>
                <Icon
                    onPress={onGoBackPressed}
                    style={styles.icon}
                    name={"arrow-left"}
                    size={20}
                />
                <Text style={styles.title}> Create new account </Text>
             </View>

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

            <CustomMsgBox text={message} />

            {!isSubmitting ?
                <CustomButton 
                text="Register"
                onPress={onRegisterPressed}
                type="primary"
            />
            :
            <CustomButton
                text={<ActivityIndicator size="small" color="white" />}>
            </CustomButton>
            }

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
    header: {
        width: "100%",
        flex: 1,
        flexDirection: "row",
        marginBottom: 25,
        textAlign: "center"
    },
    icon: {
        marginTop: "1.5%",
        width: "20%",
    },
    title: {
        width: "85%",
        fontSize: 24,
        fontWeight: 'bold',
        color: '#051C60',
        textAlign: "left",
    },
    toggle: {
        transform: [{ scaleX: 1.6}, {scaleY: 1.6}],
        marginRight: "5%",
        marginBottom: "5%"
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